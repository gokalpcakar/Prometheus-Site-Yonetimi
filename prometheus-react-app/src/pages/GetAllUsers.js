import React, { useState, useEffect } from 'react'
import { Link } from 'react-router-dom'
import axios from 'axios';

function GetAllUsers() {

    const [users, setUsers] = useState([])

    useEffect(() => {

        axios.get(`https://localhost:5001/api/User`)
            .then(response => {

                setUsers(response.data.list)
            })
            .catch(err => {
                console.log(err);
            });
    }, [users])

    const deleteHandler = async (id) => {

        const baseURL = `https://localhost:5001/api/User/${id}`;

        await fetch(baseURL, {

            method: 'DELETE',
            headers: { 'Content-Type': 'application/json' },
            credentials: 'include'
        });
    }

    return (
        <div className='container pt-5'>
            <div className='row'>
                <div className='col-12'>
                    <h1 className="text-primary text-center">Kullanıcılar</h1>
                    <Link to='/register' className='btn btn-lg btn-primary mb-4 float-end'>
                        Kullanıcı Ekle
                    </Link>
                    <table className="table table-dark table-striped">
                        <thead>
                            <tr className='text-center'>
                                <th>Ad-Soyad</th>
                                <th>E-Posta</th>
                                <th>Telefon Numarası</th>
                                <th>TC Kimlik Numarası</th>
                                <th>Plaka Numarası</th>
                                <th>Detay</th>
                                <th>Sil</th>
                            </tr>
                        </thead>
                        <tbody>
                            {users && users.map((user) => {
                                return (
                                    <tr key={user.id} className='text-center'>
                                        <td>{user.name} {user.surname}</td>
                                        <td>{user.email}</td>
                                        <td>{user.phone}</td>
                                        <td>{user.tc}</td>
                                        <td>{user.plateNo}</td>
                                        <td>
                                            <Link to={`/userdetail/${user.id}`}
                                                className="btn btn-primary">
                                                Detay
                                            </Link>
                                        </td>
                                        <td>
                                            <button
                                                className='btn btn-danger'
                                                onClick={() => deleteHandler(user.id)}
                                            >
                                                Sil
                                            </button>
                                        </td>
                                    </tr>
                                );
                            })}
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    )
}

export default GetAllUsers
