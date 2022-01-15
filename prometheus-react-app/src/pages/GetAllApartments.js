import React, { useState, useEffect } from 'react'
import axios from 'axios';

function GetAllApartments() {

    const [apartments, setApartments] = useState([])

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

        axios.get(`https://localhost:5001/api/User/ApartmentUsers`)
            .then(response => {

                setApartments(response.data)
            })
            .catch(err => {
                console.log(err);
            });
    }

    return (
        <div className='container pt-5'>
            <div className='row'>
                <div className='col-12'>
                    <table className="table table-dark table-striped">
                        <thead>
                            <tr>
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
                                    <tr key={apartment.id}>
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
                                        <td className='text-center'>{apartment.apartmentFloor}</td>
                                        <td className='text-center'>{apartment.apartmentNo}</td>
                                        <td className='text-center'>
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
