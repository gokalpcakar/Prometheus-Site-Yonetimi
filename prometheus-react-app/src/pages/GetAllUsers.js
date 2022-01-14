import React, { useState, useEffect } from 'react'
import axios from 'axios';

function GetAllUsers() {

    const [users, setUsers] = useState([])

    useEffect(() => {

        axios.get(`https://localhost:5001/api/User`)
            .then(response => {

                console.log(response.data);
                setUsers(response.data.list)
            })
            .catch(err => {
                console.log(err);
            });
    }, [])

    return (
        <div className='container pt-5'>
            <div className='row'>
                <div className='col-12'>
                    <table className="table table-dark table-striped">
                        <thead>
                            <tr>
                                <th>Name</th>
                                <th>Surname</th>
                                <th>E-Posta</th>
                                <th>Telefon Numarası</th>
                                <th>TC Kimlik Numarası</th>
                                <th>Plaka Numarası</th>
                            </tr>
                        </thead>
                        <tbody>
                            {users && users.map((user) => {
                                return (
                                    <tr key={user.id}>
                                        <td>{user.name}</td>
                                        <td>{user.surname}</td>
                                        <td>{user.email}</td>
                                        <td>{user.phone}</td>
                                        <td>{user.tc}</td>
                                        <td>{user.plateNo}</td>
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
