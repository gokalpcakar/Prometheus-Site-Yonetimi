import React, { useState, useEffect } from 'react'
import { useParams, Link } from 'react-router-dom'

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

    return (
        <div>
            <div className='container py-5 h-100'>
                <div className='row'>
                    <div className='col-6 offset-3 p-3 border rounded'>
                        <h4 className='mb-4 text-primary'>Kullanıcı Bilgileri</h4>
                        <p><b>Ad-Soyad:</b> {name} {surname}</p>
                        <p><b>E-Posta</b>: {email}</p>
                        <p><b>Telefon Numarası:</b> {phone}</p>
                        <p><b>TC kimlik numarası:</b> {tc}</p>
                        <p><b>Plaka No:</b> {plateNo}</p>
                        <p>
                            <b>Kullanıcı Tipi:</b> {isAdmin ? "Admin" : "Normal Kullanıcı"}
                        </p>
                        <Link to={`/edituser/${id}`} className="btn btn-primary float-end">
                            Kullanıcıyı Düzenle
                        </Link>
                    </div>
                </div>
            </div>
        </div>
    )
}

export default UserDetail
