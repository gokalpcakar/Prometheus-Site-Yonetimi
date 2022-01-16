import React, { useState, useEffect } from 'react'
import { Navigate, Link } from 'react-router-dom'

function EditProfile({ user }) {

    const [id, setId] = useState('')
    const [name, setName] = useState('')
    const [surname, setSurname] = useState('')
    const [email, setEmail] = useState('')
    const [phone, setPhone] = useState('')
    const [tc, setTc] = useState('')
    const [plateNo, setPlateNo] = useState('')
    const [redirect, setRedirect] = useState(false)

    useEffect(() => {

        const baseURL = `https://localhost:5001/api/User/${user.id}`;

        (
            async () => {

                const response = await fetch(baseURL, {

                    headers: { 'Content-Type': 'application/json' },
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
            }
        )();
    }, [user.id])

    const submitEvent = async (e) => {

        e.preventDefault();

        const baseURL = 'https://localhost:5001/api/User/EditProfile'

        await fetch(baseURL, {

            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                id,
                name,
                surname,
                email,
                phone,
                tc,
                plateNo
            })
        });

        setRedirect(true);
    }

    if (redirect) {

        return <Navigate to={"/myprofile"} />
    }

    return (
        <div className='container py-5 h-100'>
            <div className='row d-flex justify-content-center align-items-center h-100'>
                <div className='col-12 col-md-8 col-lg-6 col-xl-5'>
                    <div className="card shadow-2-strong" style={{ borderRadius: "1rem" }}>
                        <div className="card-body p-5 text-center">
                            <form onSubmit={submitEvent}>
                                <h1 className="h3 mb-3 fw-normal">Profil Güncelleme</h1>

                                <div className="form-floating">
                                    <input
                                        className="form-control mb-2"
                                        id="floatingName"
                                        placeholder="Name"
                                        defaultValue={name}
                                        onChange={(e) => setName(e.target.value)}
                                    />
                                    <label htmlFor="floatingName">Ad</label>
                                </div>

                                <div className="form-floating">
                                    <input
                                        className="form-control my-2"
                                        id="floatingSurName"
                                        placeholder="Surname"
                                        defaultValue={surname}
                                        onChange={(e) => setSurname(e.target.value)}
                                    />
                                    <label htmlFor="floatingSurName">Soyad</label>
                                </div>

                                <div className="form-floating">
                                    <input
                                        type="email"
                                        className="form-control my-2"
                                        id="floatingEmail"
                                        placeholder="Email"
                                        defaultValue={email}
                                        onChange={(e) => setEmail(e.target.value)}
                                    />
                                    <label htmlFor="floatingEmail">E-posta adresi</label>
                                </div>

                                <div className="form-floating">
                                    <input
                                        type="tel"
                                        className="form-control my-2"
                                        id="floatingPhone"
                                        placeholder="Phone"
                                        defaultValue={phone}
                                        onChange={(e) => setPhone(e.target.value)}
                                    />
                                    <label htmlFor="floatingPhone">Telefon Numarası</label>
                                </div>

                                <div className="form-floating">
                                    <input
                                        className="form-control my-2"
                                        id="floatingTc"
                                        placeholder="Tc"
                                        defaultValue={tc}
                                        onChange={(e) => setTc(e.target.value)}
                                    />
                                    <label htmlFor="floatingTc">TC Kimlik Numarası</label>
                                </div>

                                <div className="form-floating">
                                    <input
                                        className="form-control my-2"
                                        id="floatingPlateNo"
                                        placeholder="PlateNo"
                                        defaultValue={plateNo}
                                        onChange={(e) => setPlateNo(e.target.value)}
                                    />
                                    <label htmlFor="floatingPlateNo">Plaka Numarası</label>
                                </div>

                                <Link to="/login" className="text-center">
                                    Hesabınız mı var?
                                </Link>

                                <button className="mt-3 w-100 btn btn-lg btn-primary" type="submit">Gönder</button>
                            </form></div>
                    </div>
                </div>
            </div>
        </div>
    )
}

export default EditProfile
