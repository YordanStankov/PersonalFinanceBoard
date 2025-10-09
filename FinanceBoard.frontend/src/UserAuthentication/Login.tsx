import type { LoginDTO } from "../Models/DTOs/LoginDTO"
import type {User} from "../Models/User"

const loginData: LoginDTO = {email: "", password: ""};   
const currUser : User = {id: "", userName: "", email: ""};  
function submitHandler(data : FormData){
       
        const email = data.get("email");
        const password = data.get("password");
        if(typeof email === "string" && typeof password === "string") {
            loginData.email = email;
            loginData.password = password;
            alert(`Email: ${loginData.email}, Password: ${loginData.password}`);
            console.log("Starting fetch");
            fetch('https://localhost:7010/api/User/Login', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(loginData)
            })
            .then(response => {
                console.log(response);
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }
                else if (response.ok){
                    alert("Login successful!");
                }
                return response.json();
            }).then(data => {
                currUser.id = data.UserId;
                currUser.userName = data.UserName;
                currUser.email = data.Email;
                currUser.JWT = data.Token;
                console.log("Fetched user:", currUser);
                alert(`Logged in as: ${currUser.userName}, Email: ${currUser.email}`);
                localStorage.setItem('token', currUser.JWT ?? '');
                localStorage.setItem('User', JSON.stringify(currUser));
            }).catch(error => {
                console.error('There was a problem with the fetch operation:', error);
                alert("Login failed. Please check your credentials and try again. error: " + error.message);
            })
        }
}
export default function LoginForm() {
    return (
        <>
        <div>
            <h1>Login Page</h1>
            <form method="post" onSubmit={function(event){
                event.preventDefault();
                const formData = new FormData(event.currentTarget);
                submitHandler(formData);
            }}>
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
                <button type="submit">Log in</button>
            </form>
        </div>
        </>
    )
    }
    

