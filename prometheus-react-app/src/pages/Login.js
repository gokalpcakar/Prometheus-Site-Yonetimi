import React, { useState } from 'react'
import { Navigate } from 'react-router-dom'
import { Link } from "react-router-dom"

function Login({ setName, setUser }) {

    const [email, setEmail] = useState('')
    const [password, setPassword] = useState('')
    const [redirect, setRedirect] = useState(false)

    const submitHandler = async (e) => {

        e.preventDefault()

        const baseURL = 'https://localhost:5001/api/User/Login';

        const response = await fetch(baseURL, {

            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Accept': 'application/json'
            },
            credentials: 'include',
            body: JSON.stringify({
                email,
                password
            })
        });

        const content = await response.json()

        setRedirect(true)
        setName(content.entity.name)
        setUser(content.entity)
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
                                <h1 className="h3 mb-3 text-center fw-normal">Giriş</h1>

                                <div className="form-floating mt-4">
                                    <input
                                        type="email"
                                        className="form-control"
                                        id="floatingInput"
                                        placeholder="Email"
                                        onChange={e => setEmail(e.target.value)}
                                    />
                                    <label htmlFor="floatingInput">E-Posta</label>
                                </div>
                                <div className="form-floating mt-4">
                                    <input
                                        type="password"
                                        className="form-control"
                                        id="floatingPassword"
                                        placeholder="Password"
                                        onChange={e => setPassword(e.target.value)}
                                    />
                                    <label htmlFor="floatingPassword">Şifre</label>
                                </div>

                                <button className="w-100 btn btn-lg btn-primary my-4" type="submit">Giriş Yap</button>
                                <Link to="/forgotPassword" className="text-center">
                                    Şifremi Unuttum?
                                </Link>
                                <hr className="my-4" />

                                <Link to="/register" className="w-100 btn btn-lg btn-success">
                                    Yeni Hesap Oluştur
                                </Link>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    )
}

export default Login
