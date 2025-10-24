import React, { type JSX } from 'react';
import type { CategoryList } from '../Models/DTOs/Category/CategoryList';
import type { TransactionList } from '../Models/DTOs/Transaction/TransactionList';
import type { UserProfileDTO } from '../Models/DTOs/User/UserProfileDTO';
import { jwtDecode } from 'jwt-decode';
import { jsx } from 'react/jsx-runtime';


// var time : string = "";
var categories : CategoryList[] = [];

// function TransformDate(DateTransform: Date){
//     const options : Intl.DateTimeFormatOptions = {timeZone : 'America/New_York', year : 'numeric', month : 'long', day : 'numeric', hour : '2-digit', minute : '2-digit', second : '2-digit'}
//     const formatedDate = new Intl.DateTimeFormat('en-US', options).format(DateTransform)
//     const formatedDateString : string = formatedDate.toString()
//     console.log(formatedDateString)
//     time = formatedDateString;
// }

 async function GetUserProfile( ) : Promise<UserProfileDTO | void>{
    var profile : UserProfileDTO = { userName: ""};
    var id : string = "";

    const user = localStorage.getItem(`User`);
    const jsonUser = JSON.parse(user ?? "user is null");
    console.log(jsonUser);
    id = jsonUser.id;

    const token = localStorage.getItem(`token`) ?? "jwtToken";
    const credentials = jwtDecode(token)
    console.log(credentials);
    

   var response =  await fetch("https://localhost:7010/api/User/LoadProfile", {
        method: 'POST',
        headers: { 'Content-Type': 'application/json',
                    'Accept': 'application/json'},
        body : JSON.stringify(id)})

    if(response.ok){
        console.log(response)
        const data = await response.json();
        console.log(data)
        profile.userName = data.userName;
        profile.MonthlyIncome = data.monthlyIncome;
        profile.AverageDailySpending = data.averageDailySpending;
        profile.MonthtlySpending = data.monthlySpending;
        profile.Categories = data.categories;
        categories = data.categories;
        console.log(profile)
        // TransformDate(data.catezgories[0].transactionListDTOs[0].timeOfTransaction)
    }
    else if (!response.ok){
        console.log(response.status)
        console.log(response)
    }
    return await profile;
}

function DisplayCategoryTransactions(Transactions : TransactionList[]){
    if(!Transactions || Transactions.length === 0){
        return <h2>No transactions for this user.</h2>;
    }
    else{
            for(var i = 0; i < Transactions.length; i++){
            return <>
            <h2>Amount: {Transactions[i].Amount}</h2>
            <h2>Category: {Transactions[i].CategoryName}</h2>
            <h2>Description: {Transactions[i].Description}</h2>
            <h2>Time of transaction: dd..yyyy</h2>
            <h2>Id: {Transactions[i].Guid}</h2>
        </>
        }
    }
    
}

function DisplayUserCategories(categories: CategoryList[]) : JSX.Element[] {
    // var [passed, setPassed] = React.useState<boolean>(false);
    var [jsxElements, setjsxElements] = React.useState<JSX.Element[]>([]);
    // if(passed === true){
    //      jsxElements;
    // }
    // if(categories.length === 0 || !categories){
    //      jsxElements;
    // }
    if(!categories || categories.length === 0){
        return [<h2 key="NoCategories">No categories for this user.</h2>];
    }
        categories.map((entry) => {
            console.log("Entry in DisplayUserCategories:", entry);
            setjsxElements(jsxElements.concat(<div key={entry.Name}>
               <h2>{entry.Name} </h2>
                   <div className="TransactionsContainer">
                   {/* {DisplayCategoryTransactions(entry.TransactionList)} */}
                   </div>
           </div>));
        });
        return jsxElements;
        // setPassed(passed = true);
        // return elements;


        // for(integer; integer < categories.length - 1; integer++){
        //     setInteger(integer = integer + 1);
        //     console.log("Integer value in DisplayUserCategories:", integer);
        //       return <>
        //       <div>
        //          <h2>{categories[integer].Name} </h2>
        //              <div className="TransactionsContainer">
        //              {DisplayCategoryTransactions(categories[integer].TransactionList)}
        //              </div>
        //      </div>
        //      </>
              
        // }

        //     while(true){
                
        //     setInteger(integer = integer + 1);
        //         console.log("Integer value:", integer);
        //           var tempJsx = <>
        //     <div>
        //          <h2>{categories[integer].Name} </h2>
        //             <div className="TransactionsContainer">
        //             {DisplayCategoryTransactions(categories[integer].TransactionList)}
        //             </div>
        //     </div>
        //     </>;
        //         if(integer >= categories.length - 1){ {
        //             break;
        //         }
        // }
    // }
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
    var [jsxElements, setjsxElements] = React.useState<JSX.Element[]>([]);
        setjsxElements(jsxElements = (DisplayUserCategories(profile.Categories ?? [])));
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
            {jsxElements}
        </div>
</>
    );
}

export default Profile;
