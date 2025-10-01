export type Transaction = {
    id: string;
    amount: number;
    date: Date;
    description: string;
    categoryId: string;
    categoryName?: string; 
    userId: string;
    userName?: string;
}