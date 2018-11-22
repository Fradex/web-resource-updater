export class Connection {

  constructor(
    public id?: string,
    public name?: number,
    public connectionString?: string,
    public organizationName?: string,
    public login?: string,
    public password?: string,
    public createdOn?: Date) {
  }
}
