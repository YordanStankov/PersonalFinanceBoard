import React from 'react';
import type { CategoryList } from '../Models/DTOs/Category/CategoryList';
import type { TransactionList } from '../Models/DTOs/Transaction/TransactionList';
import type { UserProfileDTO } from '../Models/DTOs/User/UserProfileDTO';
import { jwtDecode } from 'jwt-decode';

 async function GetUserProfile( ) : Promise<UserProfileDTO | void>{
    var profile : UserProfileDTO = { userName: ""};
    var id : string = "";

    const user = localStorage.getItem(`User`);
    const jsonUser = JSON.parse(user ?? "user is null");
    console.log(jsonUser);
    id = jsonUser.id;

    const token = localStorage.getItem(`Token`) ?? "jwtToken";
    const credentials = jwtDecode(token)
    console.log(credentials);
    

   var response =  await fetch("https://localhost:7010/api/User/LoadProfile", {
        method: 'POST',
        headers: { 'Authorization' : `Bearer ${token}`,
                    'Content-Type': 'application/json',
                    'Accept': 'application/json'},
        body : JSON.stringify(id)})
        console.log(response.status + " " + response)
    const data = await response.json();
    if(response.ok){
        console.log(data)
        profile.userName = data.userName;
        profile.MonthlyIncome = data.monthlyIncome;
        profile.AverageDailySpending = data.averageDailySpending;
        profile.MonthtlySpending = data.monthlySpending;
        profile.Categories = data.categories;
        console.log(profile)
    }
    else if (!response.ok){
       console.log(data)
       alert("Error: " + data.error)
    }
    return await profile;
}

function SetProfile(data : React.Dispatch<React.SetStateAction<UserProfileDTO>>) {
    React.useEffect(() => {
        async function fetchProfile() {
            const profileData =  await GetUserProfile();
            console.log("Fetched profile data:", profileData);
            if(profileData){
                data(profileData);
            }
        }
        fetchProfile();
    }, []
    );
};

function Profile() {
    const [profile, setProfile] =  React.useState<UserProfileDTO>({userName: ""});
         SetProfile(setProfile);
        
    return (
        <>
            <div className="profile-container">
            <h2>UserName : {profile.userName}</h2>
            <h2>Montlhy spending: {profile.MonthtlySpending}</h2>
            <h2>Monthly income: {profile.MonthlyIncome}</h2>
            <h2>Avergae daily spending: {profile.AverageDailySpending}</h2>
        </div>

        <div className='categories-container'>
            <h2>Categories and their transactions: </h2>
            {profile.Categories?.map((entry : CategoryList) => {
                console.log("Entry in DisplayUserCategories:", entry);
                return <div key={entry.name}>
                       <h2>{entry.name} </h2>
                       <div className="TransactionsContainer">
                       {entry.transactionList?.map((transaction : TransactionList) => {
                        console.log("Transaction in DisplayUserCategories:", transaction);
                        return <div key={transaction.guid} className={entry.guid.toString()}>
                            <h3>Amount: {transaction.amount}</h3>
                            <h3>Category: {transaction.categoryName}</h3>
                            <h3>Description: {transaction.description}</h3>
                            <h3>Time of transaction: {new Date(transaction.timeOfTransaction).toLocaleString()}</h3>
                            <h3>Id: {transaction.guid}</h3>
                        </div>
                       })}
                       </div>
               </div>
            })}
        </div>
</>
    );
}

export default Profile;
