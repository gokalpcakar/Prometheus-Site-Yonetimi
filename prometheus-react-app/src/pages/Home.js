import React, { useState, useEffect } from 'react'
import axios from 'axios'

function Home({ name }) {

    const [users, setUsers] = useState([])

    // irtibat numarası olarak adminlerin telefon numarasını göstermek istiyoruz
    // burada adminlerin bilgisini alıyoruz
    useEffect(() => {

        axios.get('https://localhost:5001/api/User/AdminUsers')
            .then(response => {

                setUsers(response.data.list)
            })
            .catch(err => {
                console.log(err);
            })
    }, [])

    return (

        <div className='container py-5'>
            <div className='row'>
                <div className='col-12'>
                    {
                        name
                            // sisteme giriş yapılması durumunda aşağıdaki koşul devreye girecektir
                            ?
                            <div className="alert alert-success" role="alert">
                                <h4 className="alert-heading">Merhaba, {name}!</h4>
                                <p>
                                    Prometheus'a hoşgeldiniz. İşlemlerinizi gerçekleştirmek için sağ üst menüyü kullanabilirsiniz.
                                </p>
                                <hr />
                                <p className="mb-0">
                                    <b>
                                        Sistemimizle alakalı bir sıkıntı yaşamanız durumunda, sağ üst menüden bize mesaj yollayabilirsiniz.
                                    </b>
                                </p>
                            </div>
                            // sisteme giriş yapılmadıysa aşağıdaki koşul devreye girecek ve adminlerin telefon numaraları irtibat numarası olarak gösterilecektir
                            :
                            <div>
                                <div className="alert alert-danger" role="alert">
                                    <h4 className="alert-heading">Merhaba!</h4>
                                    <p>
                                        İşlemlerinizi gerçekleştirmek için giriş yapmanız gerekmektedir. Sağ üst kısımdan giriş yap seçeneğine basarak giriş ekranına geçebilirsiniz.
                                    </p>
                                    <hr />
                                    <p className="mb-0">
                                        <b>
                                            Sistemimizle alakalı bir sıkıntı yaşamanız durumunda, aşağıdaki irtibat numaralarıyla iletişime geçiniz.
                                        </b>
                                    </p>
                                </div>
                                <div>
                                    {
                                        users && users.map((user) => {

                                            return (
                                                <p key={user.id}>
                                                    {user.name} {user.surname}: <b>0{user.phone}</b>
                                                </p>
                                            )
                                        })
                                    }
                                </div>
                            </div>
                    }
                </div>
            </div>
        </div>
    )
}

export default Home
