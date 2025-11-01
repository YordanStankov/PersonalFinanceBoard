import './Css/Register.css'
import type { RegisterDTO } from "../Models/DTOs/Authentication/RegisterDTO"
import type { User } from "../Models/User";

const registerData: RegisterDTO = {userName: "", email: "", password: ""};
const user : User = {id: "", userName: "", email: "", JWT: "", Transactions: [], Categories: []};

function isPasswordValid(password: string): boolean {
    const CapitalCheck = new RegExp('[A-Z]');
    const numberCheck = new RegExp('[0-9]');
    const specialCharCheck = new RegExp('[^A-Za-z0-9]');
        if(CapitalCheck.test(password))
            if(numberCheck.test(password))
                if(specialCharCheck.test(password))
                    return true;
    return false;
}

function passwordChecker(password: string): boolean {
    if(!isPasswordValid(password)){
        alert("Password must contain at least one uppercase letter, one number, and one special character." +  "\n You entered: " + password);
        return false;
        }

    else if(password.length < 6){
        alert("Password must be at least 6 characters long.");
        return false;
    }
    else{
        return true;
    }
}
   
async function RegisterRequest(event: FormData){

    const userName = event.get("userName");
    const email = event.get("email");
    const password = event.get("password");

    if((typeof userName === "string" && typeof email === "string" && typeof password === "string") 
        && (userName.length > 0 && email.length > 0 && password.length > 6)){ 
        registerData.userName = userName;
        registerData.email = email;
        registerData.password = password;
    }
    if(!passwordChecker(registerData.password)) 
        return;
    else{
        alert(`Username: ${registerData.userName}, Email: ${registerData.email}, Password: ${registerData.password}`);
        var response =  await fetch('https://localhost:7010/api/User/Register', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json',
                'Accept': 'application/json'},
            body: JSON.stringify(registerData)
        })
        var data = await response.json();
        if(!response.ok){
            console.log("Fetch status:", response.status);
            alert("Registration failed. " + data.error);
        }
        else if(response.ok){
            alert("Registration successful! You can now log in with your credentials.");
            console.log(data);
            localStorage.clear();
            user.id = data.userId;
            user.userName = data.userName;
            user.email = data.email;
            user.JWT = data.token;
            localStorage.setItem(`token`, user.JWT ?? '');
            console.log(localStorage.getItem(`token`) ?? "jwtMissing")
            localStorage.SetItem('User', user ?? 'User not available');

        }
    }
}

 function RegisterForm() {
    return (
        <div className='RegisterForm'>
        <header className='Header'>
                <h1 className='HeaderText'>Register</h1>
        </header>
        <form method="post" onSubmit={function(event){
            event.preventDefault();
            const formData = new FormData(event.currentTarget);
            RegisterRequest(formData);
        }}>
            
            <label>
                Username:
                <input type="text" name="userName" />
            </label>
            <br />
            <label>
                Email:
                <input type="text" name="email" />
            </label>
            <br />
            <label>
                Password:
                <input type="password" name="password" />
            </label>
            <br />
            <button type="submit">Register</button>
        </form>
        </div>
    );
};

export default RegisterForm;