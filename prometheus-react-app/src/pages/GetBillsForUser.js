import React, { useEffect, useState } from 'react'
import { Link } from 'react-router-dom'
import moment from 'moment'
import axios from 'axios'

function GetBillsForUser({ user }) {

    const [bills, setBills] = useState([])

    useEffect(() => {

        axios.get(`https://localhost:5001/api/Bill/GetBillsForUser/${user.id}`)
            .then(response => {

                setBills(response.data.list)
            })
            .catch(err => {
                console.log(err);
            });
    })

    return (
        <div>
            <div className='container py-5 h-100'>
                <div className='row'>
                    <div className='col-6 offset-3 p-3 border rounded'>
                        <h4 className='mb-4 text-primary'>Faturalarım</h4>
                        {
                            bills.length < 1 ?
                                <div className="alert alert-success" role="alert">
                                    <b>Borcunuz bulunmamaktadır.</b>
                                </div>
                                :
                                null
                        }
                        {
                            bills ?
                                bills.map((bill, index) => {

                                    return (
                                        <div key={index} className="card w-100 mb-4">
                                            <div className="card-body">
                                                <h5 className="card-title">
                                                    {moment(bill.idate).format("DD.MM.YYYY")} tarihli fatura
                                                </h5>
                                            </div>
                                            <ul className="list-group list-group-flush">
                                                <li className="list-group-item">
                                                    Ad-Soyad: {user.name} {user.surname}
                                                </li>
                                                <li className="list-group-item">
                                                    Fatura türü: {bill.billType} Faturası
                                                </li>
                                                <li className="list-group-item">
                                                    Tutar: {bill.price}₺
                                                </li>
                                            </ul>
                                            <div className="card-body text-center">
                                                <Link to="/payment" className="btn btn-lg btn-primary">
                                                    Faturayı Öde
                                                </Link>
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
