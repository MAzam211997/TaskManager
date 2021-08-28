import { IGenericRepository } from "../GenericRepository/IGenericRepository";
import { Login } from "../../models/Login/Login.model";

export interface ILoginRepository extends IGenericRepository<Login> {
  Authentication(login: Login);
}
