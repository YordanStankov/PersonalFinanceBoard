import { type Transaction } from "./Transaction";
import { type Category } from "./Category";

export type User ={
    id: string;
    userName: string;
    email: string;
    Transactions?: Transaction[];
    Categories?: Category[];
    JWT?: string;
}