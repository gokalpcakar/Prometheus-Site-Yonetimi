import { useState } from 'react'
import { Navigate } from 'react-router-dom'
import React from 'react'

function AddApartment() {

    const [blockName, setBlockName] = useState('')
    const [apartmentType, setApartmentType] = useState('')
    const [apartmentNo, setApartmentNo] = useState('')
    const [apartmentFloor, setApartmentFloor] = useState()

    const [redirect, setRedirect] = useState(false)

    const submitHandler = async (e) => {

        e.preventDefault()

        const baseURL = 'https://localhost:5001/api/Bill';

        const response = await fetch(baseURL, {

            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            credentials: 'include',
            body: JSON.stringify({
                blockName,
                apartmentType,
                apartmentNo,
                apartmentFloor
            })
        });

        await response.json()

        setRedirect(true)
    }

    // submit işlemi gerçekleşmişse tüm konut bilgilerinin listelendiği ekrana geçiş sağlıyoruz
    if (redirect) {

        return <Navigate to={`/getallapartments`} />
    }
    return (
        <div className='container py-5 h-100'>
            <div className='row d-flex justify-content-center align-items-center h-100'>
                <div className='col-12 col-md-8 col-lg-6 col-xl-5'>
                    <div className="card shadow-2-strong" style={{ borderRadius: "1rem" }}>
                        <div className="card-body p-5 text-center">
                            <form onSubmit={submitHandler}>
                                <h1 className="h3 mb-3 text-center fw-normal">Konut Ekleme</h1>

                                <div className="form-floating mt-4">
                                    <input
                                        className="form-control"
                                        id="floatingBlockName"
                                        placeholder="BlockName"
                                        onChange={e => setBlockName(e.target.value)}
                                    />
                                    <label htmlFor="floatingBlockName">Hangi Blokta</label>
                                </div>
                                <div className="form-floating mt-4">
                                    <input
                                        className="form-control"
                                        id="floatingApartmentType"
                                        placeholder="ApartmentType"
                                        onChange={e => setApartmentType(e.target.value)}
                                    />
                                    <label htmlFor="floatingApartmentType">Konut Tipi</label>
                                </div>
                                <div className="form-floating mt-4">
                                    <input
                                        className="form-control"
                                        id="floatingApartmentFloor"
                                        placeholder="ApartmentFloor"
                                        onChange={e => setApartmentFloor(e.target.value)}
                                    />
                                    <label htmlFor="floatingApartmentFloor">Bulunduğu Kat</label>
                                </div>
                                <div className="form-floating mt-4">
                                    <input
                                        className="form-control"
                                        id="floatingApartmentNo"
                                        placeholder="ApartmentNo"
                                        onChange={e => setApartmentNo(e.target.value)}
                                    />
                                    <label htmlFor="floatingApartmentNo">Daire No</label>
                                </div>

                                <button className="w-100 btn btn-lg btn-primary my-4" type="submit">
                                    Ekle
                                </button>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    )
}

export default AddApartment
