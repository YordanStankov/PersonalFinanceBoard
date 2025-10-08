
import { useState } from 'react';
import reactLogo from './assets/react.svg';
import viteLogo from '/vite.svg';
import './App.css';
import { Routes, Route, useNavigate} from 'react-router-dom';
import Profile from './User/Profile.tsx';
import LoginForm from './UserAuthentication/Login.tsx';

function UserButton(){
    const navigate = useNavigate();
    return <button id='UserProfileButton' onClick={ function PressButton(){
      navigate('/user/profile');
    } }>User Button</button>
  }

   function LoginButton(){
    const navigate = useNavigate();
    return <button onClick={function PressButton(){
      navigate('/login,register/login');
    }}>LoginButton</button>
  }
function App() {
  const [count, setCount] = useState(0);
  return (
    <>
        <Routes>
          <Route path="/user/profile" element={<Profile />} />
          <Route path="/userauthentication/login" element={< LoginForm />} />
        </Routes>
    
      <div>
        <a href="https://vite.dev" target="_blank">
          <img src={viteLogo} className="logo" alt="Vite logo" />
        </a>
        <a href="https://react.dev" target="_blank">
          <img src={reactLogo} className="logo react" alt="React logo" />
        </a>
      </div>
      <h1>Vite + React</h1>
      <div className="card">
        <button onClick={() => setCount((count) => count + 1)}>
          count is {count}
        </button>
        <p>
          Edit <code>src/App.tsx</code> and save to test HMR
        </p>
      </div>
      <p className="read-the-docs">
        Wow i am programing in typescript!
      </p>
      <UserButton />
      <LoginButton />
    </>
  );
}

export default App;
