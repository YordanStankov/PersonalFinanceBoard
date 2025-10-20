import type { TransactionList } from "../Transaction/TransactionList";

export type CategoryList = {
    guid : string;
    Name : string;
    TransactionList : TransactionList[];
}