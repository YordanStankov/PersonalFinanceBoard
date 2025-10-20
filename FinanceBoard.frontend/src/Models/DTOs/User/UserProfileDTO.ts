import type { CategoryList } from "../Category/CategoryList";

export type UserProfileDTO = {
    userName? : string;
    MonthtlySpending? : number;
    MonthlyIncome? : number;
    AverageDailySpending? : number;
    exception? : string;
    Categories? : CategoryList[];
}