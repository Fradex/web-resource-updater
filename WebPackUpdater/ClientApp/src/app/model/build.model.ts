export class Build {

  constructor(
    public id?: string,
    public buildName?: string,
    public buildDescription?: string,
    public buildStatusType?: BuildStatus) {
  }
}

enum BuildStatus {
  Processing,
  ExitWithErrors,
  ExitSuccess
}
