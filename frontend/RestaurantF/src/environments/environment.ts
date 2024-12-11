import { generalSetting } from "./general.settings";

export const environemnt = {
    environemntName: 'development',
    production: false,
    API_BASE_URL: 'https://localhost:32771',
    maxUploadFileSize: 50000000,
    ...generalSetting,
}