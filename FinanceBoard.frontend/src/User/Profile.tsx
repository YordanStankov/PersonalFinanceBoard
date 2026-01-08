import React from 'react';
import type { CategoryList } from '../Models/DTOs/Category/CategoryList';
import type { UserProfileDTO } from '../Models/DTOs/User/UserProfileDTO';
import  VariabeNames from '../Constants';
import '../User/Css/Profile.css'
import type { TransactionList } from '../Models/DTOs/Transaction/TransactionList';
import {  redirectDocument } from 'react-router-dom';

const variables: VariabeNames = new VariabeNames();
var guid :string;

function SeeMoreButton(){
     localStorage.setItem(variables.categoryGuid, guid);
    return <button onClick={function PressButton(){
        redirectDocument('/category/cotegoryfocus');
    }}>
    See More
    </button>
}
 async function GetUserProfile( ) : Promise<UserProfileDTO>{
    var profile : UserProfileDTO = { userName: ""};
    var id : string = "";

    const user = localStorage.getItem(`${variables.user}`);
    const jsonUser = JSON.parse(user ?? "user is null");
    id = jsonUser.id;
    const token = localStorage.getItem(`${variables.token}`) ?? "jwtToken";
    var response =  await fetch("https://localhost:7010/api/User/LoadProfile", {
        method: 'POST',
        headers: { 'Authorization' : `Bearer ${token}`,
                    'Content-Type': 'application/json',
                    'Accept': 'application/json'},
        body : JSON.stringify(id)})
        console.log(response)
    const data = await response.json();
    if(response.ok){
        profile.userName = data.userName;
        profile.MonthlyIncome = data.monthlyIncome;
        profile.AverageDailySpending = data.averageDailySpending;
        profile.MonthlySpendingAverage = data.monthlySpendingAverage;
        profile.Categories = data.categories;
    }
    else if (!response.ok){
        profile.exception = data.error;
       alert("Error: " + data.error)
    }
    return await profile;
}

function SetProfile(data : React.Dispatch<React.SetStateAction<UserProfileDTO>>) {
    React.useEffect(() => {
        async function fetchProfile() {
            const profileData =  await GetUserProfile();
            if(profileData){
                data(profileData);
            }
        }
        fetchProfile();
    }, []
    );
}

const profile : UserProfileDTO = await GetUserProfile();
console.log(profile);
function DisplayTransactions(data : TransactionList[])
{
    if(!data.length)
        return <h2>No transactions available</h2>
    else{
        return <ul>
            {data.map((transaction : TransactionList)=>{
                return <li key={transaction.guid}>
                       <h4>Guid: {transaction.guid}</h4>
                       <h4>Amount: {transaction.amount}€</h4>
                  </li>
            })}
        </ul>
    }
}

function DisplayCategories(data : CategoryList[]){
    if(!data.length)
        return <h2>No categories created</h2>;
    else{
        return <ul>
            {data.map((category : CategoryList) =>{
                return <li key={category.guid}>
                    <h3>Category: {category.name}</h3>
                    {DisplayTransactions(category.transactionListDTOs)}
                </li>
            })}
        </ul>
    };
}

const UserInfo = () => {
    return <>
        <div className="profile-container">
            <h2>UserName : {profile.userName}</h2>
            <h2>Montlhy spending: {profile.MonthlySpendingAverage}€</h2>
            <h2>Monthly income: {profile.MonthlyIncome}€</h2>
            <h2>Avergae daily spending: {profile.AverageDailySpending}€</h2>
        </div>
    </>
}
const CategoriesSection = () => {
    return  <div className="categories-section">
                <h2>Categories</h2>
                {DisplayCategories(profile.Categories ?? [])}
            </div>

}

function Profile() {
    return (
            <>
                <UserInfo/>
                <CategoriesSection/>
            </>
    );
}

export default Profile;
