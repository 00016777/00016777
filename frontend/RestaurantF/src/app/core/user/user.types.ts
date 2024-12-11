
export interface User
{
    id?: number;
    userName?: string;
    fullName?: string;
    mainRoleId?: number;
    roles?: string[];
    email?: string;
    imageUrl?: string;
}