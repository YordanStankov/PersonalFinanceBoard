import React, { type JSX } from 'react';
import type { CategoryList } from '../Models/DTOs/Category/CategoryList';
import type { UserProfileDTO } from '../Models/DTOs/User/UserProfileDTO';
import  VariabeNames from '../Constants';
import '../User/Css/Profile.css'
import type { TransactionList } from '../Models/DTOs/Transaction/TransactionList';
import {  redirectDocument, useNavigate } from 'react-router-dom';
import type { UserInfoDTO } from '../Models/DTOs/User/UserInfoDTO';

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
    const data = await response.json();
    if(response.ok){
        profile.userName = data.userName;
        profile.MonthlyIncome = data.monthlyIncome;
        profile.AverageDailySpending = data.averageDailySpending;
        profile.MonthtlySpending = data.monthlySpending;
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
};

const profile : UserProfileDTO = await GetUserProfile();

function returnNum(data : TransactionList[]):number{
    try{
        return data.length;
    }
    catch{
        return 0;
    }
}

function DisplayCategories(data : CategoryList){
            const numberOfTransaction : number = returnNum(data.transactionListDTOs);
        return <>
           <h2>Category: {data.name} </h2>
           <h2 className='number-of-transactions'>Transactions: {numberOfTransaction}</h2>
           {guid = data.guid}
            <SeeMoreButton/>
        </>  
}

const UserInfo = () => {
    // const [userInfo, setUser] = React.useState<UserInfoDTO>();
    // const Setter = () =>{
    //     var info : UserInfoDTO =  {
    //         userName: profile.userName,
    //         AverageDailySpending : profile.AverageDailySpending,
    //         MonthlyIncome: profile.MonthlyIncome,
    //         MonthtlySpending: profile.MonthtlySpending
    //     }
    //     setUser(info); 
    // }
    // Setter();
    return <>
    <div className="profile-container">
        
                <h2>UserName : {profile.userName}</h2>
                <h2>Montlhy spending: {profile.MonthtlySpending}</h2>
                <h2>Monthly income: {profile.MonthlyIncome}</h2>
                <h2>Avergae daily spending: {profile.AverageDailySpending}</h2>
            </div>
    </>
}

const CategoriesSection = () => {
    // const [categories, setCategories] = React.useState<CategoryList[]>();
    // setCategories(profile.Categories);
    return <>
    <div className='categories-container'>
            <h2 className='header-of-categories'>Categories and their transactions: </h2>
            {profile.Categories?.map((data : CategoryList) => {
               return  <div key={data.guid}>
                    {DisplayCategories(data)}
                    </div>  
            })}
        </div>
    </>
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
