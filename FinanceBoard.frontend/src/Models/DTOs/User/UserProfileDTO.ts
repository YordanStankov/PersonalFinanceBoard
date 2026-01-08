import type { CategoryList } from "../Category/CategoryList";

export type UserProfileDTO = {
    userName? : string;
    MonthlySpendingAverage? : number;
    MonthlyIncome? : number;
    AverageDailySpending? : number;
    exception? : string;
    Categories? : CategoryList[];
}