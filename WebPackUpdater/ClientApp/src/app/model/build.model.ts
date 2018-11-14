export class Build {

  constructor(
    public id?: number,
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
