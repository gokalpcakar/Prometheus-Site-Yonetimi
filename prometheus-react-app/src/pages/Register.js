import React, { useState, useEffect } from 'react'
import { Navigate, Link } from 'react-router-dom'
import axios from "axios"

function Register() {

    const [name, setName] = useState('')
    const [surname, setSurName] = useState('')
    const [email, setEmail] = useState('')
    const [phone, setPhone] = useState('')
    const [tc, setTc] = useState('')
    const [plateNo, setPlateNo] = useState('')
    const [password, setPassword] = useState('')
    const [redirect, setRedirect] = useState(false)

    const [apartments, setApartments] = useState([])
    const [apartmentId, setApartmentId] = useState([])

    useEffect(() => {

        // new user only see empty apartments
        axios.get('https://localhost:5001/api/Apartment/EmptyApartments')
            .then(response => {

                setApartments(response.data.list)
            })
            .catch(err => {
                console.log(err);
            })
    }, [])

    const submitEvent = async (e) => {

        e.preventDefault();
        
        const baseURL = 'https://localhost:5001/api/User'

        await fetch(baseURL, {

            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                name,
                surname,
                email,
                phone,
                password,
                tc,
                plateNo,
                apartmentId
            })
        });

        setRedirect(true);
    }

    const handleSelectChange = (e) => {

        setApartmentId(e.target.value);
    }

    if (redirect) {

        return <Navigate to={"/login"} />
    }

    return (
        <div className='container py-5 h-100'>
            <div className='row d-flex justify-content-center align-items-center h-100'>
                <div className='col-12 col-md-8 col-lg-6 col-xl-5'>
                    <div className="card shadow-2-strong" style={{ borderRadius: "1rem" }}>
                        <div className="card-body p-5 text-center">
                            <form onSubmit={submitEvent}>
                                <h1 className="h3 mb-3 fw-normal">Kayıt Olun</h1>

                                <div className="form-floating">
                                    <input
                                        className="form-control mb-2"
                                        id="floatingName"
                                        placeholder="Name"
                                        onChange={(e) => setName(e.target.value)}
                                    />
                                    <label htmlFor="floatingName">Ad</label>
                                </div>

                                <div className="form-floating">
                                    <input
                                        className="form-control my-2"
                                        id="floatingSurName"
                                        placeholder="Surname"
                                        onChange={(e) => setSurName(e.target.value)}
                                    />
                                    <label htmlFor="floatingSurName">Soyad</label>
                                </div>

                                <div className="form-floating">
                                    <input
                                        type="email"
                                        className="form-control my-2"
                                        id="floatingEmail"
                                        placeholder="Email"
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
                                        onChange={(e) => setPhone(e.target.value)}
                                    />
                                    <label htmlFor="floatingPhone">Telefon Numarası</label>
                                </div>

                                <div className="form-floating">
                                    <input
                                        className="form-control my-2"
                                        id="floatingTc"
                                        placeholder="Tc"
                                        onChange={(e) => setTc(e.target.value)}
                                    />
                                    <label htmlFor="floatingTc">TC Kimlik Numarası</label>
                                </div>

                                <div className="form-floating">
                                    <input
                                        className="form-control my-2"
                                        id="floatingPlateNo"
                                        placeholder="PlateNo"
                                        onChange={(e) => setPlateNo(e.target.value)}
                                    />
                                    <label htmlFor="floatingPlateNo">Plaka Numarası</label>
                                </div>

                                <div className="form-floating">
                                    <input
                                        type="password"
                                        className="form-control my-2"
                                        id="floatingPassword"
                                        placeholder="Password"
                                        onChange={(e) => setPassword(e.target.value)}
                                    />
                                    <label htmlFor="floatingPassword">Şifre</label>
                                </div>

                                <div className='mb-3'>
                                    <select
                                        className="form-select"
                                        aria-label="Default select example"
                                        onChange={handleSelectChange}
                                    >
                                        <option defaultValue={"Daireyi seçiniz"}>Daireyi seçiniz...</option>
                                        {apartments && apartments.map((apartment) => {
                                            return (
                                                <option key={apartment.id} value={apartment.id}>
                                                    {apartment.blockName} blok,
                                                    Tipi: {apartment.apartmentType},
                                                    No: {apartment.apartmentNo},
                                                    Kat: {apartment.apartmentFloor}
                                                </option>
                                            );
                                        })}
                                    </select>
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

export default Register
