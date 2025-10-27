import type { CreateCategory } from '../Models/DTOs/Category/CreateCategory';

const categoryData: CreateCategory = {userID: "", name: ""};

async function CreateCategoryRequest(event: FormData){
    const user = localStorage.getItem('User');
    var userId:string = " ";
    var name:string = " ";
    if(user!== null){
        const userJson = JSON.parse(user);
        userId = userJson.id;
    }
     name = event.get("name") as string;
    if(name.length > 0 && userId.length > 0){
        categoryData.name = name;
        categoryData.userID = userId;
    }
    else{
        alert("Please provide a valid category name.");
        return;
    }
    alert(`UserID: ${categoryData.userID}, Category Name: ${categoryData.name}`);
    var response = await fetch('https://Localhost:7010/api/Category/CreateCategory',{
        method: 'POST',
        headers: { 'Content-Type': 'application/json',
            'Accept': 'application/json'},
        body: JSON.stringify(categoryData)
    })
    if(!response.ok){
        console.log("Fetch status:", response.status);
        alert("Category creation failed. Please try again. Status: " + response.status);
    }
    else if(response.ok){
        alert("Category created successfully!");
        var data = await response.json();
        console.log(data);
    }
}

function CreateCategoryForm(){
    return <div>
        <header>
            <h1>Create Category</h1>
        </header>
        <form method="post" onSubmit={function(event){
            event.preventDefault();
            const formData = new FormData(event.currentTarget);
            CreateCategoryRequest(formData);
        }}>
            <label>
                Category Name:
                <input type="text" name="name" />
            </label>
            <br />
            <button type="submit">Create</button>
        </form>
    </div>
}
export default CreateCategoryForm;