import React, { useState, useEffect } from 'react'
import axios from 'axios';
import moment from 'moment';

function GetAllBills() {

    const [bills, setBills] = useState([])

    // iki farklı tablodan veri gerektiği için join işlemi yapılmış bir api çağırıldı
    useEffect(() => {

        axios.get(`https://localhost:5001/api/Bill/UserBills`)
            .then(response => {

                setBills(response.data)
            })
            .catch(err => {
                console.log(err);
            });
    }, [bills])

    const deleteHandler = async (id) => {

        const baseURL = `https://localhost:5001/api/Bill/${id}`;

        await fetch(baseURL, {

            method: 'DELETE',
            headers: { 'Content-Type': 'application/json' },
            credentials: 'include'
        });
    }

    return (
        <div className='container pt-5'>
            <div className='row'>
                <div className='col-12'>
                    <h1 className="text-primary text-center mb-4">Fatura ve Aidatlar</h1>
                    <table className="table table-dark table-striped">
                        <thead>
                            <tr className='text-center'>
                                <th>Ad-Soyad</th>
                                <th>Alacak Türü</th>
                                <th>Tutar</th>
                                <th className='text-center'>Eklenme Tarihi</th>
                                <th className='text-center'>
                                    Son Ödeme Tarihi
                                </th>
                                <th>Sil</th>
                            </tr>
                        </thead>
                        <tbody>
                            {bills && bills.map((bill) => {
                                return (
                                    <tr key={bill.id} className='text-center'>
                                        <td>{bill.name} {bill.surname}</td>
                                        <td>{bill.billType}</td>
                                        <td>{bill.price} ₺</td>
                                        <td className='text-center'>
                                            {moment(bill.idate).format("DD.MM.YYYY")}
                                        </td>
                                        <td className='text-center'>
                                            {moment(bill.dueDate).format("DD.MM.YYYY")}
                                        </td>
                                        <td>
                                            <button
                                                type='button'
                                                className='btn btn-danger'
                                                onClick={() => deleteHandler(bill.id)}
                                            >
                                                Sil
                                            </button>
                                        </td>
                                    </tr>
                                );
                            })}
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    )
}

export default GetAllBills
