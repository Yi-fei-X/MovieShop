// Login in response model, the one we get from API
export interface User{
    nameid: number;     //userid
    family_name: string;
    given_name: string;
    email: string;
    role: Array<string>;
    exp: string;
    alias: string;
    isAdmin: boolean;
    birthdate: Date;   
}