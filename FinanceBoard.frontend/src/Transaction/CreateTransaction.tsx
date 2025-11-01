import type { CreateTransactionDTO } from "../Models/DTOs/Transaction/CreateTransactionDTO";


const transactionData : CreateTransactionDTO = {
    amount: 0,
    date: new Date(),
    categoryName: "",
    description: "",
    userId: ""
}   
async function SendCreationRequest(event : FormData) {
    const userJson = JSON.parse(localStorage.getItem('User') ?? "{}");
    const userId:string = userJson.id ?? "";

    var amount: number;
    var date: Date; 
    var description: string;
    var categoryName: string;

    const amoutString = event.get("amount") as string; 
    amount = parseFloat(amoutString);
    const dateString = event.get("date") as string;
    date = new Date(dateString);
    categoryName = event.get("categoryName") as string;
    description = event.get("description") as string;
    if(userId.length > 0 && !isNaN(amount) && categoryName.length > 0 && date !== null){
        transactionData.userId = userId;
        transactionData.amount = amount;
        transactionData.date = date;
        transactionData.categoryName = categoryName;
        transactionData.description = description;
        var response = await fetch('https://localhost:7010/api/Transaction/CreateTransaction',{
            method: 'POST',
            headers: { 'Content-Type': 'application/json',
                'Accept': 'application/json'},
            body: JSON.stringify(transactionData)
        })  
        var data = await response.json();
        if(!response.ok){
            alert("Transaction creation failed. Please try again. Status: " + data.error);
        }
        else if(response.ok){
            alert("Transaction created successfully! Guid of transaction: " + data.transactionGuid);
            console.log(data);
        }
    }
    else {
        alert("Please provide valid transaction details.");
        return;
    }
}

function CreateTransactionForm(){
    return <div>
        <header>
            <h1>Create Transaction</h1>
        </header>
        <form method="post" onSubmit={function(event){
            event.preventDefault();
            const formData = new FormData(event.currentTarget);
            SendCreationRequest(formData);
        }}>
            <label>
                Amount:
                <input type="number" name="amount" step="0.01" />
            </label>
            <br />
            <label>
                Date:
                <input type="date" name="date" />
            </label>
            <br />
            <label>
                Category Name:
                <input type="text" name="categoryName" />
            </label>
            <br />
            <label>
                Description:
                <input type="text" name="description" />
            </label>
            <br />
            <button type="submit">Create</button>
        </form>

    </div>
}
export default CreateTransactionForm;