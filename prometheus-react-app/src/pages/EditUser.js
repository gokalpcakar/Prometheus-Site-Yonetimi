import React, { useState, useEffect } from 'react'
import { useParams, Navigate, Link } from 'react-router-dom'
import axios from 'axios'

function EditUser() {

    const params = useParams()

    const [id, setId] = useState('')
    const [name, setName] = useState('')
    const [surname, setSurname] = useState('')
    const [email, setEmail] = useState('')
    const [phone, setPhone] = useState('')
    const [password, setPassword] = useState('')
    const [tc, setTc] = useState('')
    const [plateNo, setPlateNo] = useState('')
    const [isAdmin, setIsAdmin] = useState(false)

    const [apartments, setApartments] = useState([])
    const [apartmentId, setApartmentId] = useState([])

    const [redirect, setRedirect] = useState()

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
                setPassword(content.entity.password)
                setTc(content.entity.tc)
                setPlateNo(content.entity.plateNo)
                setIsAdmin(content.entity.isAdmin)
                setApartmentId(content.entity.apartmentId)
            }
        )();
    }, [params.id])

    useEffect(() => {

        axios.get('https://localhost:5001/api/Apartment')
            .then(response => {

                setApartments(response.data.list)
            })
            .catch(err => {
                console.log(err);
            })
    }, [])

    const handleApartmentChange = (e) => {

        setApartmentId(e.target.value);
    }

    const handleAdminChange = (e) => {

        setIsAdmin(e.target.value);
    }

    const submitEvent = async (e) => {

        e.preventDefault();

        const baseURL = 'https://localhost:5001/api/User'

        await fetch(baseURL, {

            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                id,
                name,
                surname,
                email,
                phone,
                password,
                tc,
                plateNo,
                isAdmin,
                apartmentId
            })
        });

        setRedirect(true);
    }

    if (redirect) {

        return <Navigate to={"/getallusers"} />
    }

    return (
        <div className='container py-5 h-100'>
            <div className='row d-flex justify-content-center align-items-center h-100'>
                <div className='col-12 col-md-8 col-lg-6 col-xl-5'>
                    <div className="card shadow-2-strong" style={{ borderRadius: "1rem" }}>
                        <div className="card-body p-5 text-center">
                            <form onSubmit={submitEvent}>
                                <h1 className="h3 mb-3 fw-normal">Kullanıcı Düzenleme</h1>

                                <div className="d-none">
                                    <input
                                        defaultValue={id}
                                    />
                                </div>

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

                                <div className="d-none">
                                    <input
                                        type="password"
                                        defaultValue={password}
                                    />
                                </div>

                                <div className='mb-3'>
                                    <select
                                        className="form-select"
                                        aria-label="Default select example"
                                        onChange={handleAdminChange}
                                    >
                                        <option value={isAdmin}>
                                            Kullanıcı tipini Seçiniz...
                                        </option>
                                        <option value={true}>
                                            Admin
                                        </option>
                                        <option value={false}>
                                            Kullanıcı
                                        </option>
                                    </select>
                                </div>

                                <div className='mb-3'>
                                    <select
                                        className="form-select"
                                        aria-label="Default select example"
                                        onChange={handleApartmentChange}
                                    >
                                        <option defaultValue={apartmentId}>
                                            Daireyi Seçiniz...
                                        </option>
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

export default EditUser
