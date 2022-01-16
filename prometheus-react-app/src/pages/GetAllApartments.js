import React, { useState, useEffect } from 'react'
import { Link } from 'react-router-dom';
import axios from 'axios';

function GetAllApartments() {

    const [apartments, setApartments] = useState([])

    // iki farklı tablodan veri gerektiği için join işlemi yapılmış bir api çağırıldı
    useEffect(() => {

        axios.get(`https://localhost:5001/api/User/ApartmentUsers`)
            .then(response => {

                setApartments(response.data)
            })
            .catch(err => {
                console.log(err);
            });
    }, [apartments])

    const deleteHandler = async (id) => {

        const baseURL = `https://localhost:5001/api/Apartment/${id}`;

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
                    <h1 className="text-primary text-center">Konutlar</h1>
                    <Link to='/addapartment' className='btn btn-lg btn-primary mb-4 float-end'>
                        Konut Ekle
                    </Link>
                    <table className="table table-dark table-striped">
                        <thead>
                            <tr className='text-center'>
                                <th>Ad-Soyad</th>
                                <th>Blok Adı</th>
                                <th>Doluluk</th>
                                <th>Konut Tipi</th>
                                <th className='text-center'>Kat</th>
                                <th className='text-center'>Daire No</th>
                                <th className='text-center'>Sil</th>
                            </tr>
                        </thead>
                        <tbody>
                            {apartments && apartments.map((apartment) => {
                                return (
                                    <tr className='text-center' key={apartment.id}>
                                        <td>{apartment.name} {apartment.surname}</td>
                                        <td>{apartment.blockName} Blok</td>
                                        <td>
                                            {
                                                apartment.isFull ?
                                                    "Dolu" :
                                                    "Boş"
                                            }
                                        </td>
                                        <td>{apartment.apartmentType}</td>
                                        <td>{apartment.apartmentFloor}</td>
                                        <td>{apartment.apartmentNo}</td>
                                        <td>
                                            <button
                                                type='button'
                                                className='btn btn-danger'
                                                onClick={() => deleteHandler(apartment.apartmentId)}
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

export default GetAllApartments
