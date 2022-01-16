import React, { useState, useEffect } from 'react'
import { Link } from 'react-router-dom'
import axios from 'axios'
import moment from 'moment'

function MyMessages({ user }) {

    const [sentMessages, setSentMessages] = useState([])
    const [receivedMessages, setReceivedMessages] = useState([])

    // gönderici olunan mesajlar getiriliyor
    useEffect(() => {

        axios.get(`https://localhost:5001/api/Message/GetBySender/${user.id}`)
            .then(response => {

                setSentMessages(response.data.list)
            })
            .catch(err => {
                console.log(err);
            });
    }, [sentMessages])

    // alıcı olunan mesajlar getiriliyor
    useEffect(() => {

        axios.get(`https://localhost:5001/api/Message/GetByReceiver/${user.id}`)
            .then(response => {

                setReceivedMessages(response.data.list)
            })
            .catch(err => {
                console.log(err);
            });
    }, [receivedMessages])

    const deleteHandler = async (id) => {

        const baseURL = `https://localhost:5001/api/Message/${id}`;

        await fetch(baseURL, {

            method: 'DELETE',
            headers: { 'Content-Type': 'application/json' },
            credentials: 'include'
        });
    }

    // okundu olarak işaretle dendiğinde ilgili mesajın isRead değeri değiştiriliyor
    const readHandler = async (id) => {

        const baseURL = `https://localhost:5001/api/Message/ReadMessage/${id}`;

        await fetch(baseURL, {

            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            credentials: 'include'
        });
    }

    return (
        <div>
            <div className='container py-5 h-100'>
                <div className='row'>
                    <div className='col-10 offset-1 p-3 border rounded'>
                        <Link
                            to="/addmessage"
                            className='btn btn-primary mb-2 float-end'>
                            Mesaj Ekle
                        </Link>
                        <h4 className='mb-4 text-primary'>Gelen Mesajlar</h4>
                        {
                            receivedMessages.length > 0 ?
                                receivedMessages.map((receivedMessage, index) => {

                                    return (
                                        <div key={index} className="card w-100 my-4">
                                            <div className="card-body">
                                                <h5 className="card-title">
                                                    {moment(receivedMessage.idate).format("DD.MM.YYYY")} tarihli mesaj
                                                    {
                                                        receivedMessage.isNewMessage
                                                            ?
                                                            <span className='text-danger float-end'>
                                                                Yeni Mesaj
                                                            </span>
                                                            :
                                                            null
                                                    }
                                                </h5>
                                            </div>
                                            <ul className="list-group list-group-flush">
                                                <li className="list-group-item">
                                                    Gönderici: <b>{receivedMessage.senderName} {receivedMessage.senderSurname}</b>
                                                </li>
                                                <li className="list-group-item">
                                                    Alıcı: <b>{receivedMessage.receiverName} {receivedMessage.receiverSurname}</b>
                                                </li>
                                                <li className="list-group-item">
                                                    {
                                                        receivedMessage.isRead
                                                            ?
                                                            <p className='text-success'>
                                                                Bu mesaj okundu
                                                            </p>
                                                            :
                                                            <p className='text-danger'>
                                                                Bu mesaj okunmadı
                                                            </p>
                                                    }
                                                </li>
                                                <li className="list-group-item">
                                                    <h4 className='mt-2 mb-3'>Mesaj İçeriği</h4>
                                                    <hr />
                                                    <p>
                                                        {
                                                            receivedMessage.messageContent
                                                        }
                                                    </p>
                                                    <p className='float-end'>
                                                        <button
                                                            onClick={() => readHandler(receivedMessage.id)}
                                                            className="btn btn-primary">
                                                            Okundu olarak işaretle
                                                        </button>
                                                        <button
                                                            onClick={() => deleteHandler(receivedMessage.id)}
                                                            className="btn btn-danger ms-2">
                                                            Mesajı Sil
                                                        </button>
                                                    </p>
                                                </li>
                                            </ul>
                                        </div>
                                    );
                                }) :
                                <p className='text-danger'>
                                    Size gönderilen bir mesaj bulunmamaktadır
                                </p>
                        }
                    </div>

                    <div className='col-10 offset-1 mt-4 p-3 border rounded'>
                        <h4 className='mb-4 text-primary'>Gönderilen Mesajlar</h4>
                        {
                            sentMessages.length > 0 ?
                                sentMessages.map((sentMessage, index) => {

                                    return (
                                        <div key={index} className="card w-100 my-4">
                                            <div className="card-body">
                                                <h5 className="card-title">
                                                    {moment(sentMessage.idate).format("DD.MM.YYYY")} tarihli mesaj
                                                    {
                                                        sentMessage.isNewMessage
                                                            ?
                                                            <span className='text-danger float-end'>
                                                                Yeni Mesaj
                                                            </span>
                                                            :
                                                            null
                                                    }
                                                </h5>
                                            </div>
                                            <ul className="list-group list-group-flush">
                                                <li className="list-group-item">
                                                    Gönderici: <b>{sentMessage.senderName} {sentMessage.senderSurname}</b>
                                                </li>
                                                <li className="list-group-item">
                                                    Alıcı: <b>{sentMessage.receiverName} {sentMessage.receiverSurname}</b>
                                                </li>
                                                <li className="list-group-item">
                                                    {
                                                        sentMessage.isRead
                                                            ?
                                                            <p className='text-success'>
                                                                Bu mesaj okundu
                                                            </p>
                                                            :
                                                            <p className='text-danger'>
                                                                Bu mesaj okunmadı
                                                            </p>
                                                    }
                                                </li>
                                                <li className="list-group-item">
                                                    <h4 className='mt-2 mb-3'>Mesaj İçeriği</h4>
                                                    <hr />
                                                    <p>
                                                        {
                                                            sentMessage.messageContent
                                                        }
                                                    </p>
                                                    <p className='float-end'>
                                                        <button
                                                            onClick={() => readHandler(sentMessage.id)}
                                                            className="btn btn-primary">
                                                            Okundu olarak işaretle
                                                        </button>
                                                        <button
                                                            onClick={() => deleteHandler(sentMessage.id)}
                                                            className="btn btn-danger ms-2">
                                                            Mesajı Sil
                                                        </button>
                                                    </p>
                                                </li>
                                            </ul>
                                        </div>
                                    );
                                }) :
                                <p className='text-danger'>
                                    Gönderdiğiniz bir mesaj bulunmamaktadır
                                </p>
                        }
                    </div>
                </div>
            </div>
        </div>
    )
}

export default MyMessages
