import React, { useState, useEffect } from 'react'
import { Link } from 'react-router-dom'
import moment from 'moment'
import axios from 'axios'

function PaidBills({ user }) {

    const [bills, setBills] = useState([])

    useEffect(() => {

        axios.get(`https://localhost:5001/api/Bill/GetPaidBillsForUser/${user.id}`)
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
                    <div className='col-10 offset-1 p-3 border rounded'>
                        <h4 className='mb-4 text-primary'>Faturalarım</h4>
                        {
                            bills.length < 1 ?
                                <div className="alert alert-danger" role="alert">
                                    <b>Ödenmiş faturanız bulunmamaktadır.</b>
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

export default PaidBills
