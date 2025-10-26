import type { LoginDTO } from "../Models/DTOs/Authentication/LoginDTO"
import type {User} from "../Models/User"
import './Css/Login.css'

const loginData: LoginDTO = {email: "", password: ""};   
const currUser : User = {id: "", userName: "", email: "", JWT: "", Transactions: [], Categories: []};  
async function submitHandler( event : FormData){

        const email = event.get("email");
        const password = event.get("password");

        if((typeof email === "string" && typeof password === "string") && (email.length > 0 && password.length > 0)){ 
            loginData.email = email;
            loginData.password = password;

            alert(`Email: ${loginData.email}, Password: ${loginData.password}`);
            console.log("Starting fetch");

           var result =  await fetch('https://localhost:7010/api/User/Login', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json',
                    'Accept': 'application/json'},
                body: JSON.stringify(loginData)
            })

            console.log("Fetch status:", result.status);

            if(!result.ok){
                alert("Login failed. Please check your credentials and try again. Status: " + result.status);
            }

            else if(result.ok){
                try{
                alert("Login successful!");
                const data = await result.json();
                currUser.id = data.userId;
                currUser.userName = data.userName;
                currUser.email = data.email;
                currUser.JWT = data.token;
                alert(`Logged in as: ${currUser.userName}, Email: ${currUser.email}`);
                localStorage.clear();
                localStorage.setItem(`token`, currUser.JWT ?? '');
                console.log(localStorage.getItem(`token`) ?? "jwtMissing")
                localStorage.setItem('User', JSON.stringify(currUser));
                } catch(error){
                    alert("An error occured while proccesing your request:" + (error as Error).message);
                }
            }
            // .then(response => {
            //     if (!response.ok) {
            //         throw new Error('Network response was not ok');
            //     }
            //     else if (response.ok){
            //         alert("Login successful!");
            //     }
            //     return response.json();
            // }).then( data=> {
            //     currUser.id = data.userId;
            //     currUser.userName = data.userName;
            //     currUser.email = data.email;
            //     currUser.JWT = data.token;
            //     console.log("Fetched user:", currUser);
            //     alert(`Logged in as: ${currUser.userName}, Email: ${currUser.email}`);
            //     localStorage.setItem('token', currUser.JWT ?? '');
            //     localStorage.setItem('User', JSON.stringify(currUser));
            // }).catch(error => {
            //     console.error('There was a problem with the fetch operation:', error);
            //     alert("Login failed. Please check your credentials and try again. error: " + error.message);
            // })
        }
        else {
            alert("Invalid input. Please enter valid email and password.");
        }
}

export default function LoginForm() {
    return (
        <>
        <div className="LoginForm">
            <header>
                <h1>Login Page</h1> 
            </header>
            
            <form method="post" onSubmit={function(event){
                event.preventDefault();
                const formData = new FormData(event.currentTarget);
                submitHandler(formData);
            }}>
                <div>
                <label>
                    Email:
                    <input type="text" name="email" />
                </label>
                <br />
                </div>
               <div>
                <label>
                    Password:
                    <input type="password" name="password" />
                </label>
                <br />
                </div> 
                
                <button type="submit">Log in</button>
            </form>
        </div>
        </>
    )
}
    

