import React, { useState, useEffect } from 'react'
import { Navigate } from 'react-router-dom'
import axios from 'axios'

function AddMessage({ user }) {

    const senderId = user.id
    const [receiverId, setReceiverId] = useState()
    const [messageContent, setMessageContent] = useState('')
    const [users, setUsers] = useState([])
    const [adminUsers, setAdminUsers] = useState([])
    const [redirect, setRedirect] = useState(false)

    useEffect(() => {

        axios.get('https://localhost:5001/api/User')
            .then(response => {

                setUsers(response.data.list)
            })
            .catch(err => {
                console.log(err);
            })
    }, [])

    useEffect(() => {

        axios.get('https://localhost:5001/api/User/AdminUsers')
            .then(response => {

                setAdminUsers(response.data.list)
            })
            .catch(err => {
                console.log(err);
            })
    }, [])

    const handleSelectChange = (e) => {

        setReceiverId(e.target.value)
    }

    const submitHandler = async (e) => {

        e.preventDefault()

        const baseURL = 'https://localhost:5001/api/Message';

        const response = await fetch(baseURL, {

            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            credentials: 'include',
            body: JSON.stringify({
                senderId,
                receiverId,
                messageContent
            })
        });

        const content = await response.json()

        setRedirect(true)
    }

    if (redirect) {

        return <Navigate to={"/mymessages"} />
    }

    return (
        <div className='container py-5 h-100'>
            <div className='row d-flex justify-content-center align-items-center h-100'>
                <div className='col-12 col-md-8 col-lg-6 col-xl-5'>
                    <div className="card shadow-2-strong" style={{ borderRadius: "1rem" }}>
                        <div className="card-body p-5 text-center">
                            <form onSubmit={submitHandler}>
                                <h1 className="h3 mb-3 text-center fw-normal">Mesaj Ekleme</h1>

                                <div className='mb-3'>
                                    <select
                                        className="form-select"
                                        aria-label="Default select example"
                                        onChange={handleSelectChange}
                                    >
                                        <option>Kime</option>
                                        {user.isAdmin ?
                                            users && users.map((user) => {
                                                return (
                                                    <option key={user.id} value={user.id}>
                                                        {user.name} {user.surname}
                                                    </option>
                                                );
                                            }) :
                                            adminUsers && adminUsers.map((admin) => {
                                                return (
                                                    <option key={admin.id} value={admin.id}>
                                                        {admin.name} {admin.surname}
                                                    </option>
                                                );
                                            })
                                        }
                                    </select>
                                </div>

                                <div className="form-group mt-4">
                                    <textarea class="form-control" id="exampleFormControlTextarea1" rows="3" placeholder='Mesajınızı giriniz...' onChange={e => setMessageContent(e.target.value)}></textarea>
                                </div>

                                <button className="w-100 btn btn-lg btn-primary my-4" type="submit">
                                    Gönder
                                </button>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    )
}

export default AddMessage
