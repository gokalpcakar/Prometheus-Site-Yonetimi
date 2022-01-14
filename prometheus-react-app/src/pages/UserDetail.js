import React, { useState, useEffect } from 'react'
import { useParams, Link } from 'react-router-dom'
import axios from 'axios'
import moment from 'moment'

function UserDetail() {

    const params = useParams()

    const [id, setId] = useState('')
    const [name, setName] = useState('')
    const [surname, setSurname] = useState('')
    const [email, setEmail] = useState('')
    const [phone, setPhone] = useState('')
    const [tc, setTc] = useState('')
    const [plateNo, setPlateNo] = useState('')
    const [isAdmin, setIsAdmin] = useState(false)
    const [bills, setBills] = useState([])

    useEffect(() => {

        const baseURL = `https://localhost:5001/api/User/${params.id}`;

        (
            async () => {

                const response = await fetch(baseURL, {

                    headers: {
                        'Content-Type': 'application/json',
                        'Accept': 'application/json'
                    },
                    credentials: 'include'
                });

                const content = await response.json()

                setId(content.entity.id)
                setName(content.entity.name)
                setSurname(content.entity.surname)
                setEmail(content.entity.email)
                setPhone(content.entity.phone)
                setTc(content.entity.tc)
                setPlateNo(content.entity.plateNo)
                setIsAdmin(content.entity.isAdmin)
            }
        )();
    }, [params.id])

    

    useEffect(() => {

        axios.get(`https://localhost:5001/api/Bill/GetUnpaidBillsForUser/${params.id}`)
            .then(response => {

                setBills(response.data.list)
            })
            .catch(err => {
                console.log(err);
            });
    }, [bills])

    return (
        <div>
            <div className='container py-5 h-100'>
                <div className='row'>
                    <div className='col-10 offset-1 p-3 border rounded'>
                        <h4 className='mb-4 text-primary'>Kullanıcı Bilgileri</h4>
                        <p><b>Ad-Soyad:</b> {name} {surname}</p>
                        <p><b>E-Posta</b>: {email}</p>
                        <p><b>Telefon Numarası:</b> {phone}</p>
                        <p><b>TC kimlik numarası:</b> {tc}</p>
                        <p><b>Plaka No:</b> {plateNo}</p>
                        <p>
                            <b>Kullanıcı Tipi:</b> {isAdmin ? "Admin" : "Kullanıcı"}
                        </p>
                        <Link to={`/addbill/${id}`} className="btn btn-success float-end ms-2">
                            Fatura Ekle
                        </Link>
                        <Link to={`/edituser/${id}`} className="btn btn-primary float-end">
                            Kullanıcıyı Düzenle
                        </Link>
                    </div>
                    <div className='col-10 offset-1 p-3 border rounded mt-4'>
                        <h4 className='mb-4 text-primary'>Faturaları</h4>
                        {
                            bills.length < 1 ?
                                <div className="alert alert-success" role="alert">
                                    <b>Fatura veya aidat borcu bulunmamaktadır.</b>
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
                                                    Ad-Soyad: {name} {surname}
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

export default UserDetail
