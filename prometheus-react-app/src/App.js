import Nav from './components/Nav';
import Home from './pages/Home';
import Login from './pages/Login';
import Register from './pages/Register';
import MyProfile from './pages/MyProfile';
import EditProfile from './pages/EditProfile';
import GetBillsForUser from './pages/GetBillsForUser';
import BillPayment from './pages/BillPayment';
import './App.css';
import { useState, useEffect } from 'react';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import GetAllUsers from './pages/GetAllUsers';

function App() {

  const [name, setName] = useState('')

  const [user, setUser] = useState({

    id: "",
    name: "",
    surname: "",
    email: "",
    phone: "",
    password: "",
    tc: "",
    plateNo: ""
  })

  useEffect(() => {

    const baseURL = 'https://localhost:5001/api/User/LoggedUser';

    (
      async () => {

        const response = await fetch(baseURL, {

          headers: {
            'Content-Type': 'application/json',
            'Accept': 'application/json'
          },
          credentials: 'include'
        });

        const content = await response.json()
        setName(content.name)
        setUser({

          id: content.id,
          name: content.name,
          surname: content.surname,
          email: content.email,
          phone: content.phone,
          password: content.password,
          tc: content.tc,
          plateNo: content.plateNo
        })
      }
    )();
  }, [])

  return (
    <div className="App">
      <BrowserRouter>
        <Nav name={name} setName={setName} />
        <main>
          <Routes>
            <Route path="/" exact element={<Home name={name} />} />
            <Route path="/login" element={<Login setName={setName} setUser={setUser} />} />
            <Route path="/register" element={<Register />} />
            <Route path="/myprofile" element={<MyProfile user={user} />} />
            <Route path="/editprofile" element={<EditProfile />} />
            <Route path="/getbillsforuser" element={<GetBillsForUser user={user} />} />
            <Route path="/payment" element={<BillPayment />} />
            <Route path="/getallusers" element={<GetAllUsers />} />
          </Routes>
        </main>
      </BrowserRouter>
    </div>
  );
}

export default App;
