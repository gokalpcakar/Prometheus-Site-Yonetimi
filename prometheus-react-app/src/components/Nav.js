import React from 'react'
import { Link } from "react-router-dom"

function Nav({ name, setName, isAdmin }) {

    const logoutHandler = async () => {

        const baseURL = 'https://localhost:5001/api/User/Logout';

        await fetch(baseURL, {

            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Accept': 'application/json'
            },
            credentials: 'include'
        });

        setName(() => "")
    }

    return (
        <div>
            <nav className="navbar navbar-expand-lg navbar-dark bg-dark">
                <div className="container-fluid">
                    <Link to="/" className="navbar-brand">Prometheus</Link>
                    <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                        <span className="navbar-toggler-icon"></span>
                    </button>
                    <div className="collapse navbar-collapse" id="navbarNav">

                        <ul className="navbar-nav ms-auto">
                            <li className="nav-item">
                                <Link to="/" className="nav-link active" aria-current="page">Anasayfa</Link>
                            </li>
                            <li className="nav-item">
                                <Link to="/register" className="nav-link">Kayıt Ol</Link>
                            </li>
                            {
                                (name === '' || name === undefined)
                                    ?
                                    <li className="nav-item">
                                        <Link to="/login" className="nav-link">Giriş Yap</Link>
                                    </li>
                                    :
                                    <li className="nav-item">
                                        <div className="dropdown">
                                            <button
                                                type="button"
                                                className="btn btn-dark rounded-circle"
                                                id="dropdownMenuButton1"
                                                data-bs-toggle="dropdown" aria-expanded="false">

                                                <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-person-fill" viewBox="0 0 16 16">
                                                    <path d="M3 14s-1 0-1-1 1-4 6-4 6 3 6 4-1 1-1 1H3zm5-6a3 3 0 1 0 0-6 3 3 0 0 0 0 6z"></path>
                                                </svg>
                                            </button>
                                            <div className="dropdown-menu dropdown-menu-end" aria-labelledby="dropdownMenuButton1">
                                                <Link to="/myprofile"
                                                    className="dropdown-item">
                                                    Profilim
                                                </Link>
                                                <Link to="/mymessages"
                                                    className="dropdown-item">
                                                    Mesajlarım
                                                </Link>
                                                {
                                                    isAdmin ?
                                                        <Link to="/getallusers"
                                                            className="dropdown-item">
                                                            Kullanıcılar
                                                        </Link>
                                                        :
                                                        null
                                                }
                                                {
                                                    isAdmin ?
                                                        <Link to="/getallapartments"
                                                            className="dropdown-item">
                                                            Konutlar
                                                        </Link>
                                                        :
                                                        null
                                                }
                                                <Link to="/getbillsforuser"
                                                    className="dropdown-item">
                                                    Faturalarım
                                                </Link>
                                                <Link
                                                    to="/login"
                                                    className="dropdown-item"
                                                    onClick={logoutHandler}>
                                                    Çıkış Yap
                                                </Link>
                                            </div>
                                        </div>
                                    </li>
                            }
                        </ul>
                    </div>
                </div>
            </nav>
        </div>
    )
}

export default Nav
