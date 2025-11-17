import './App.css';
import { Routes, Route, useNavigate} from 'react-router-dom';
import Profile from './User/Profile.tsx';
import LoginForm from './UserAuthentication/Login.tsx';
import RegisterForm from './UserAuthentication/Register.tsx';
import CreateCategoryForm from './Category/CreateCategory.tsx';
import CreateTransactionForm from './Transaction/CreateTransaction.tsx';
import CategoryFocus from './Category/CategoryFocus.tsx';


function CreateCategoryButton(){
    const navigate = useNavigate();
    return <button onClick={function PressButton(){
      navigate('/category/createcategory');
    }}>Create Category</button>
  }

function UserButton(){
    const navigate = useNavigate();
    return <button id='UserProfileButton' onClick={ function PressButton(){
      navigate('/user/profile');
    } }>User Button</button>
  }

  function RegisterButton(){
    const navigate = useNavigate();
    return <button className='RegisterButton' onClick={function PressButton(){
      navigate('/userauthentication/register');
    }}>RegisterButton</button>
  }

   function LoginButton(){
    const navigate = useNavigate();
    return <button onClick={function PressButton(){
      navigate('/userauthentication/login');
    }}>LoginButton</button>
  }

  function CreateTransactionButton(){
    const navigate  = useNavigate();
    return <button onClick={function PressButton(){
      navigate('/transaction/createtransaction');
    }}>CreateTransaction</button>
  }

function App() {
  return (
    <>
    <main className='main'>
        <Routes>
          <Route path="/category/categoryfocus" element={<CategoryFocus/>}/>
          <Route path="/user/profile" element={<Profile />} />
          <Route path="/userauthentication/register" element={< RegisterForm />} />
          <Route path="/userauthentication/login" element={< LoginForm />} />
          <Route path="/category/createcategory" element={< CreateCategoryForm />} />
          <Route path="transaction/createtransaction" element={<CreateTransactionForm/>}></Route>
        </Routes>
        
          <header className='header'>
            <div className='AuthButtons'>
              <h2>Authentication</h2>
              <LoginButton />
              <RegisterButton />  
              <UserButton />
            </div>
          </header>
          <div className='CreationButtons'>
            <h2>Creation</h2>
              <CreateCategoryButton />
              <CreateTransactionButton />
          </div>
        </main>
    </>
  );
}

export default App;
