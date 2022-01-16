import React, { useState } from 'react'
import { Navigate, useParams } from 'react-router-dom'

function AddCreditCard() {

    const [redirect, setRedirect] = useState()

    const params = useParams()
    const userId = params.id

    let creditCardId = ""
    // const [creditCardId, setCreditCardId] = useState()
    const [creditCardNo, setCreditCardNo] = useState()
    const [creditCardCVV, setCreditCardCVV] = useState()
    const [expirationDate, setExpirationDate] = useState()

    const submitHandler = async (e) => {

        e.preventDefault()

        const baseURL = 'https://localhost:5001/api/CreditCard';

        const response = await fetch(baseURL, {

            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            credentials: 'include',
            body: JSON.stringify({
                creditCardNo,
                creditCardCVV,
                expirationDate
            })
        });

        const content = await response.json()
        creditCardId = content.id
        console.log(creditCardId);
        console.log(userId);

        await fetch('https://localhost:5001/api/User/UpdateCreditCard', {

            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            credentials: 'include',
            body: JSON.stringify({
                id: userId,
                creditCardId
            })
        });

        setRedirect(true)
    }

    if (redirect) {

        return <Navigate to={"/"} />
    }

    return (
        <div className='container py-5 h-100'>
            <div className='row d-flex justify-content-center align-items-center h-100'>
                <div className='col-12 col-md-8 col-lg-6 col-xl-5'>
                    <div className="card shadow-2-strong" style={{ borderRadius: "1rem" }}>
                        <div className="card-body p-5 text-center">
                            <form onSubmit={submitHandler}>
                                <h1 className="h3 mb-3 text-center fw-normal">Kredi Kartı Ekleme</h1>

                                <div className="form-floating mt-4">
                                    <input
                                        className="form-control"
                                        id="floatingA"
                                        placeholder="CreditCardNo"
                                        onChange={e => setCreditCardNo(e.target.value)}
                                    />
                                    <label htmlFor="floatingA">Kredi Kartı Numarası</label>
                                </div>
                                <div className="form-floating mt-4">
                                    <input
                                        className="form-control"
                                        id="floatingB"
                                        placeholder="CreditCardCVV"
                                        onChange={e => setCreditCardCVV(e.target.value)}
                                    />
                                    <label htmlFor="floatingB">CVV</label>
                                </div>

                                <div className="form-floating mt-4">
                                    <input
                                        type="date"
                                        className="form-control"
                                        id="floatingC"
                                        placeholder="ExpirationDate"
                                        onChange={e => setExpirationDate(e.target.value)}
                                    />
                                    <label htmlFor="floatingC">Son Kullanma Tarihi</label>
                                </div>

                                <button className="w-100 btn btn-lg btn-primary my-4" type="submit">Ekle</button>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    )
}

export default AddCreditCard
