import Nav from './components/Nav';
import Home from './pages/Home';
import Login from './pages/Login';
import Register from './pages/Register';
import MyProfile from './pages/MyProfile';
import EditProfile from './pages/EditProfile';
import GetBillsForUser from './pages/GetBillsForUser';
import './App.css';
import { useState, useEffect } from 'react';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import GetAllUsers from './pages/GetAllUsers';
import UserDetail from './pages/UserDetail';
import EditUser from './pages/EditUser';
import AddBill from './pages/AddBill';
import PaidBills from './pages/PaidBills';
import AddCreditCard from './pages/AddCreditCard';
import GetAllApartments from './pages/GetAllApartments';
import MyMessages from './pages/MyMessages';
import AddMessage from './pages/AddMessage';

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
    plateNo: "",
    isAdmin: false
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
          plateNo: content.plateNo,
          isAdmin: content.isAdmin
        })
      }
    )();
  }, [])

  return (
    <div className="App">
      <BrowserRouter>
        <Nav name={name} setName={setName} isAdmin={user.isAdmin} />
        <main>
          <Routes>
            <Route path="/" exact element={<Home name={name} />} />
            <Route path="/login" element={<Login setName={setName} setUser={setUser} />} />
            <Route path="/register" element={<Register />} />
            <Route path="/myprofile" element={<MyProfile user={user} />} />
            <Route path="/editprofile" element={<EditProfile />} />
            <Route path="/getbillsforuser" element={<GetBillsForUser user={user} />} />
            <Route path="/paidbills" element={<PaidBills user={user} />} />
            <Route path="/getallusers" element={<GetAllUsers user={user} />} />
            <Route path="/userdetail/:id" element={<UserDetail />} />
            <Route path="/edituser/:id" element={<EditUser />} />
            <Route path="/addbill/:id" element={<AddBill />} />
            <Route path="/addcreditcard/:id" element={<AddCreditCard />} />
            <Route path="/getallapartments" element={<GetAllApartments />} />
            <Route path="/mymessages" element={<MyMessages user={user} />} />
            <Route path="/addmessage" element={<AddMessage user={user} />} />
          </Routes>
        </main>
      </BrowserRouter>
    </div>
  );
}

export default App;
