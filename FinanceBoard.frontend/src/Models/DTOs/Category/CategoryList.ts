import type { TransactionList } from "../Transaction/TransactionList";

export type CategoryList = {
    guid : string;
    name : string;
    transactionList : TransactionList[];
}