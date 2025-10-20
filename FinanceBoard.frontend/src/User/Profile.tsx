import type { CategoryList } from '../Models/DTOs/Category/CategoryList';
import type { TransactionList } from '../Models/DTOs/Transaction/TransactionList';
import type { UserProfileDTO } from '../Models/DTOs/User/UserProfileDTO';
import { jwtDecode } from 'jwt-decode';

var profile : UserProfileDTO = { userName: ""};
// var time : string = "";
var categories : CategoryList[] = [];

// function TransformDate(DateTransform: Date){
//     const options : Intl.DateTimeFormatOptions = {timeZone : 'America/New_York', year : 'numeric', month : 'long', day : 'numeric', hour : '2-digit', minute : '2-digit', second : '2-digit'}
//     const formatedDate = new Intl.DateTimeFormat('en-US', options).format(DateTransform)
//     const formatedDateString : string = formatedDate.toString()
//     console.log(formatedDateString)
//     time = formatedDateString;
// }

 async function GetUserProfile( ){
    var id : string = "";
    var user = localStorage.getItem(`User`);
    var jsonUser = JSON.parse(user ?? "user is null")
    console.log(jsonUser);
    id = jsonUser.id;

    var token = localStorage.getItem(`token`) ?? "jwtToken";
    var credentials = jwtDecode(token)
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

}

// function DisplayCategoryTransactions(Transactions : TransactionList[]){
//     for(var i = 0; i < Transactions.length; i++){
//         return <>
//          <h2>Amount: {Transactions[i].Amount}</h2>
//          <h2>Category: {Transactions[i].CategoryName}</h2>
//          <h2>Description: {Transactions[i].Description}</h2>
//          <h2>Time of transaction: {time}</h2>
//             <h2>Id: {Transactions[i].Guid}</h2>
//         </>
//     }
// }
// function DisplayUserCategories(categories: CategoryList[]){
//     for(var i = 0; i < categories.length; i++ ){
//         return <>
//         <div>
//             <h2>{categories[i].Name} </h2>
//                 <div className="TransactionsContainer">
//                     {DisplayCategoryTransactions(categories[i].TransactionList)}
//                 </div>
//         </div>
//     </>
//     }
   
// }

function Profile() {
    GetUserProfile()
    return (
        <>
        <div className="profile-container">
            <h2>UserName : {profile.userName}</h2>
            <h2>Montlhy spending: {profile.MonthtlySpending}</h2>
            <h2>Monthly income: {profile.MonthlyIncome}</h2>
            <h2>Avergae daily spending: {profile.AverageDailySpending}</h2>
        </div>
        {/* {DisplayUserCategories(profile.Categories ?? categories)}  */}
</>
    );
}

export default Profile;
