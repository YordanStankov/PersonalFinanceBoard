import type { TransactionList } from '../Models/DTOs/Transaction/TransactionList';
import type { UserProfileDTO } from '../Models/DTOs/User/UserProfileDTO';
import type { User } from '../Models/User';
import { jwtDecode } from 'jwt-decode';

const profile : UserProfileDTO = { userName: ""};
 async function GetUserProfile( ){
    var token = localStorage.getItem(`token`) ?? "jwtToken";
    var credentials = jwtDecode(token)
    console.log(token)
    await fetch("https://localhost:7010/api/User/LoadProfile", {
        method: 'GET',
        headers: { 
            'Accept': 'application/json',
        }
    }).then(response => {
        if (!response.ok) {
            console.log("Fetch status:", response.status);
            throw new Error('Network response was not ok');
        }
        else if(response.ok){
            alert("Profile loaded successfully!");
        }
    })
}


function Profile() {
    GetUserProfile()
    return (
        <>
        <div className="profile-container">
            <h2>UserName : {profile.userName}</h2>
        </div>
</>
    );
}

export default Profile;

