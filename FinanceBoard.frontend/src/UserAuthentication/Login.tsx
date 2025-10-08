import React, { use } from "react"
import type { LoginDTO } from "../Models/DTOs/LoginDTO"
import { Form } from "react-router-dom";

const loginData: LoginDTO = {username: "", password: ""};   
export default function LoginForm() {
    function submitHandler(data : FormData){
       
        const username = data.get("username");
        const password = data.get("password");
        if(typeof username === "string" && typeof password === "string") {
            loginData.username = username;
            loginData.password = password;
            alert(`Username: ${loginData.username}, Password: ${loginData.password}`);
            fetch('https://localhost:7010/api/UserController/login', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(loginData)
            })
            .then(response => {
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }
                else if (response.ok){
                    alert("Login successful!");
                }
                alert(response.json()) ;
    }
).then(data => {
                console.log('Success:', data);
            })
    }
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
                    Username:
                    <input type="text" name="username" />
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
}
