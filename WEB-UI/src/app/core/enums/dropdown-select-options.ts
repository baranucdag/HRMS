export enum isDeletedOptions {
  all = 0,
  notDeleted = 1,
  deleted = 2,
}

export enum workPlaceTypeOptions {
  all = 0,
  remote = 1,
  hybrid = 2,
  fromOffice = 3,
}

export enum departmentOptions {
  all = 0,
  system = 1,
  software = 2,
  humanResources = 3,
}

export enum department {
  system ,
  software ,
  humanResources ,
}

export enum workTimeTypeOptions {
  all = 0,
  partTime = 1,
  fullTime = 2,
  ıntern = 3,
}

export enum applicationStatusOptions{
  rejected=-1,
  evaluation=0,
  onlineMeeting=1,
  faceToFaceMeeting=2,
  offered=3,
  acceptOffer=4,
  startToWork=5
}

export enum gender {
  male = 0,
  female = 1,
  unspecified = 2,
}

export enum claims {
  admin = 1,
  hr = 2,
  user = 3,
}

export enum genderOptions {
  all = 0,
  male = 1,
  female = 2,
  unspecified = 3,
}

export enum workTimeType {
  partTime = 0,
  fullTime=1,
  intern=2,
}

export enum workPlaceTypeEnum {
  remote=0,
  hybrid=1,
  fromOffice=2,
}
