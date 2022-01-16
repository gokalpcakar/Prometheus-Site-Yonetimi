import React, { useEffect, useState } from 'react'
import { Link } from 'react-router-dom'
import moment from 'moment'
import axios from 'axios'

function GetBillsForUser({ user }) {

    const [bills, setBills] = useState([])
    const [creditCardId, setCreditCardId] = useState([])

    // admin bir kullanıcı hakkındaki fatura ya da aidatları görmek istediğinde ödenmemiş fatura ya da aidatlarını görüyor
    useEffect(() => {

        axios.get(`https://localhost:5001/api/Bill/GetUnpaidBillsForUser/${user.id}`)
            .then(response => {

                setBills(response.data.list)
            })
            .catch(err => {
                console.log(err);
            });
    }, [bills])

    // kullanıcının kredi kartı bilgisi yoksa kullanıcı ödeme işlemini gerçekleştiremiyor
    // burada setCreditCardId diyerek sistemdeki kredi kartı bilgisini alıyoruz
    useEffect(() => {

        axios.get(`https://localhost:5001/api/User/${user.id}`)
            .then(response => {

                setCreditCardId(response.data.entity.creditCardId)
            })
            .catch(err => {
                console.log(err);
            });
    }, [user])

    const billHandler = async (id) => {

        const baseURL = `https://localhost:5001/api/Bill/${id}`;

        await fetch(baseURL, {

            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            credentials: 'include'
        });
    }

    return (
        <div>
            <div className='container py-5 h-100'>
                <div className='row'>
                    <div className='col-10 offset-1 p-3 border rounded'>
                        <h4 className='mb-4 text-primary'>Faturalarım</h4>
                        <Link to="/paidbills">
                            Ödenmiş faturalarım
                        </Link>
                        {
                            bills.length < 1 ?
                                <div className="alert alert-success mt-4" role="alert">
                                    <b>Borcunuz bulunmamaktadır.</b>
                                </div>
                                :
                                null
                        }
                        {
                            bills ?
                                bills.map((bill, index) => {

                                    return (
                                        <div key={index} className="card w-100 my-4">
                                            <div className="card-body">
                                                <h5 className="card-title">
                                                    {moment(bill.idate).format("DD.MM.YYYY")} kesim tarihli alacak
                                                </h5>
                                            </div>
                                            <ul className="list-group list-group-flush">
                                                <li className="list-group-item">
                                                    Ad-Soyad: {user.name} {user.surname}
                                                </li>
                                                <li className="list-group-item">
                                                    Alacak türü: {bill.billType}
                                                </li>
                                                <li className="list-group-item">
                                                    Tutar: {bill.price}₺
                                                </li>
                                                <li className="list-group-item">
                                                    Son ödeme tarihi: {moment(bill.dueDate).format("DD.MM.YYYY")}
                                                </li>
                                            </ul>
                                            <div className="card-body text-center">
                                                {
                                                    creditCardId === null
                                                        ?
                                                        <div>
                                                            <div className="alert alert-danger mt-4" role="alert">
                                                                <b>
                                                                    Kredi kartı bilginiz bulunmamaktadır.
                                                                </b>
                                                            </div>
                                                            <Link
                                                                to={`/addcreditcard/${user.id}`}
                                                                className='btn btn-lg btn-primary mt-2'>
                                                                Kredi kartı ekle
                                                            </Link>
                                                        </div>
                                                        :
                                                        <button
                                                            type='button'
                                                            className='btn btn-primary'
                                                            onClick={() => billHandler(bill.id)}
                                                        >
                                                            Faturayı Öde
                                                        </button>
                                                }
                                            </div>
                                        </div>
                                    );
                                }) :
                                <p className='text-danger'>Lütfen tekrar deneyiniz.</p>
                        }
                    </div>
                </div>
            </div>
        </div>
    )
}

export default GetBillsForUser
