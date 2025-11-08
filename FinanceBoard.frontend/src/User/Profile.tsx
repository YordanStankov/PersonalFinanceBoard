import React from 'react';
import type { CategoryList } from '../Models/DTOs/Category/CategoryList';
import type { UserProfileDTO } from '../Models/DTOs/User/UserProfileDTO';
import { jwtDecode } from 'jwt-decode';
import  VariabeNames from '../Constants';
import '../User/Css/Profile.css'
import type { TransactionList } from '../Models/DTOs/Transaction/TransactionList';

const variables: VariabeNames = new VariabeNames();

 async function GetUserProfile( ) : Promise<UserProfileDTO | void>{
    var profile : UserProfileDTO = { userName: ""};
    var id : string = "";

    const user = localStorage.getItem(`${variables.user}`);
    const jsonUser = JSON.parse(user ?? "user is null");
    console.log(jsonUser);
    id = jsonUser.id;

    const token = localStorage.getItem(`${variables.token}`) ?? "jwtToken";
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
function returnNum(data : TransactionList[]):number{
    try{
        return data.length;
    }
    catch{
        return 0;
    }
}
function DisplayCategories(data : CategoryList){
     console.log("Entry in DisplayUserCategories:", data);
            const numberOfTransaction : number = returnNum(data.transactionListDTOs)
            if(numberOfTransaction > 0){
                 return <>
                  <div className='card-for-category'>
                  <h2>Category: {data.name} </h2>
                  <h2 className='number-of-transactions'>Transactions: {numberOfTransaction}</h2>
                   {data.transactionListDTOs?.map((transaction : TransactionList) => {
                    return <>
                    <div className='card-for-transaction'>
                        <p>Amount: {transaction.amount}</p>
                        <p>Guid: {transaction.guid}</p>
                        <p>Description: {transaction.description}</p>
                      </div>
                   </>
               })}
         </div>
      </>
   }

    else{
        return <>
        <div className='card-for-category'>
           <h2>Category: {data.name} </h2>
           <h2 className='number-of-transactions'>Transactions: {numberOfTransaction}</h2>
           </div>
        </>
   }
               
}

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
            <h2 className='header-of-categories'>Categories and their transactions: </h2>
            {profile.Categories?.map((data : CategoryList) => {
               return <>
                    {DisplayCategories(data)}
               </>
            })}
        </div>
</>
    );
}

export default Profile;
