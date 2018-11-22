"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var Connection = /** @class */ (function () {
    function Connection(id, name, connectionString, organizationName, login, password, createdOn) {
        this.id = id;
        this.name = name;
        this.connectionString = connectionString;
        this.organizationName = organizationName;
        this.login = login;
        this.password = password;
        this.createdOn = createdOn;
    }
    return Connection;
}());
exports.Connection = Connection;
//# sourceMappingURL=connection.model.js.map