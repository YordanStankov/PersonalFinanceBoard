import VariableNames from "../Constants";

const variables = new VariableNames();

async function RequestDetails(){
const guid : string = localStorage.getItem(variables.categoryGuid) ?? "Guid is null";
}

function CategoryFocus(){
   
    return <>
        <h2>Pooper poopy pooper</h2>
    </>
}
export default CategoryFocus;