import React, { useState } from 'react'
import { useParams, Navigate } from 'react-router-dom'

function AddBill() {

    const params = useParams()
    const userid = params.id

    const [billType, setBillType] = useState('')
    const [price, setPrice] = useState(0)
    const [dueDate, setDueDate] = useState(0)
    const [redirect, setRedirect] = useState(false)

    // kullanıcıya fatura ekliyoruz
    const submitHandler = async (e) => {

        e.preventDefault()

        const baseURL = 'https://localhost:5001/api/Bill';

        const response = await fetch(baseURL, {

            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            credentials: 'include',
            body: JSON.stringify({

                billType,
                price,
                userid,
                dueDate
            })
        });

        const content = await response.json()

        setRedirect(true)
        setBillType(content.entity.billType)
        setPrice(content.entity.price)
    }

    // kullanıcıysa fatura eklendikten sonra kullanıcının detay sayfasına geçiş yapıyoruz
    if (redirect) {

        return <Navigate to={`/userdetail/${userid}`} />
    }

    return (
        <div className='container py-5 h-100'>
            <div className='row d-flex justify-content-center align-items-center h-100'>
                <div className='col-12 col-md-8 col-lg-6 col-xl-5'>
                    <div className="card shadow-2-strong" style={{ borderRadius: "1rem" }}>
                        <div className="card-body p-5 text-center">
                            <form onSubmit={submitHandler}>
                                <h1 className="h3 mb-3 text-center fw-normal">Fatura Ekleme</h1>

                                <div className="form-floating mt-4">
                                    <input
                                        className="form-control"
                                        id="floatingBillType"
                                        placeholder="BillType"
                                        onChange={e => setBillType(e.target.value)}
                                    />
                                    <label htmlFor="floatingBillType">Fatura Tipi</label>
                                </div>
                                <div className="form-floating mt-4">
                                    <input
                                        className="form-control"
                                        id="floatingPrice"
                                        placeholder="Price"
                                        onChange={e => setPrice(e.target.value)}
                                    />
                                    <label htmlFor="floatingPrice">Fatura Tutarı</label>
                                </div>

                                <div className="form-floating mt-4">
                                    <input
                                        type="date"
                                        className="form-control"
                                        id="floatingC"
                                        placeholder="DueDate"
                                        onChange={e => setDueDate(e.target.value)}
                                    />
                                    <label htmlFor="floatingC">Son Ödeme Tarihi</label>
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

export default AddBill
