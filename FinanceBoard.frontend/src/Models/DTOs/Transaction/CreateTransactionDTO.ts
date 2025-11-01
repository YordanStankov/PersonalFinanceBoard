export type CreateTransactionDTO = {
    amount: number;
    date: Date;
    categoryName: string;
    description: string;
    userId: string; 
}