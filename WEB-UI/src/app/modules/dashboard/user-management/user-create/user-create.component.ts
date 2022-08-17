import { IUserOperationClaim } from './../../../../core/models/views/userOperationClaim.model';
import {
  claims,
  department,
} from './../../../../core/enums/dropdown-select-options';
import { UserOperationClaimService } from './../../../../core/services/api/user-operation-claim.service';
import { AuthService } from './../../../../core/services/api/auth.service';
import { IUser } from './../../../../core/models/views/user.model';
import { Subject, takeUntil } from 'rxjs';
import { UserService } from 'src/app/core/services/api/user.service';
import { IFormComponent } from './../../../../core/components/interfaces/form-components';
import { Component, EventEmitter, OnInit } from '@angular/core';
import { Form, FormBuilder, FormGroup, Validators } from '@angular/forms';
import {
  setSavingStatus,
  setUpdatingStatus,
} from 'src/app/core/helpers/sidebar';
import { IOnInitializingParam } from 'src/app/core/components/models';
import {
  SidebarDialogResult,
  SidebarDialogResultStatus,
} from 'src/app/core/components/dialogs/sidebar-dialog/enums';
import { IDropdownOptions } from 'src/app/core/components/dropdowns/dropdown/models';
import { enumToArray } from 'src/app/core/helpers/enum';

@Component({
  selector: 'app-user-create',
  templateUrl: './user-create.component.html',
  styleUrls: ['./user-create.component.scss'],
})
export class UserCreateComponent implements OnInit, IFormComponent {
  initialData!: { id: number };
  selectedUser: any;
  userForm!: FormGroup;
  claimDropdownOptions?: IDropdownOptions;
  selectedClaim: any;
  usersCLaim: any;

  claimTypes: any = enumToArray(claims).map((m) => {
    return { label: m.description.toCapitalize(), value: m.id };
  });

  get f(): any {
    return this.userForm.controls;
  }

  public readonly onInitializing = new Subject<IOnInitializingParam>();
  public readonly onResult = new EventEmitter<SidebarDialogResult>();
  private readonly onDestroy = new Subject<void>();

  constructor(
    private userService: UserService,
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private userOperationClaimService: UserOperationClaimService
  ) {}

  ngOnInit(): void {
    this.createForm();
  }

  //set data (id)
  setData(data: any) {
    this.initialData = data;
    if (this.initialData.id) {
      // update
      this.getUserOperationClaim(this.initialData.id);
    } else {
      // create
    }
  }

  getUser(id: number) {
    this.userService
      .getById(id)
      .pipe(takeUntil(this.onDestroy))
      .subscribe((response: any) => {
        const user = response.body.data;
        this.selectedUser = user;
        this.createForm(user);
      });
  }

  //get User by id
  getUserOperationClaim(id: number) {
    this.userOperationClaimService
      .getById(id)
      .pipe(takeUntil(this.onDestroy))
      .subscribe((response: any) => {
        const data = response.body.data
        this.selectedClaim = data;
        this.getUser(this.initialData.id);
      });
  }

  createForm(user?: IUser) {
    this.userForm = this.formBuilder.group({
      firstName: [user?.firstName, Validators.required],
      password: ['***', Validators.required],
      lastName: [user?.lastName, Validators.required],
      email: [user?.email, Validators.required],
    });
    if (this.selectedClaim) {
      this.selectedClaim = this.claimTypes.find(
        (x: any) => x.value === this.selectedClaim.operationClaimId
      );
    }
   
    
    
    this.setClaimDropdownOptions(this.claimTypes, this.selectedClaim);
  }

  //set dropdown options
  setClaimDropdownOptions(data: any, selected?: any) {
    console.log(data);
    console.log(selected);
    
    
    // if (selected == undefined) {
    //   selected = data[0];
    //   this.selectedWorkPlaceType = selected;
    // }
    this.claimDropdownOptions = {
      items: data,
      onSelectionChange: (value) => {
        this.selectedClaim = value;
      },
      optionLabel: 'label',
      placeholder: 'selected',
      selected: selected,
    };
    this.claimDropdownOptions.errors?.next([]);
  }

  //add operation
  save() {
    Object.keys(this.userForm.controls).forEach((key) => {
      this.userForm.get(key)?.markAsDirty();
    });

    if (this.userForm.invalid) {
      return;
    }

    this.userForm.disable();

    setSavingStatus(this.onInitializing, true);

    let sendModel: IUser = Object.assign({}, this.userForm.value);
    const sendForm = new FormData();
    sendForm.append('email', sendModel.email);
    sendForm.append('firstName', sendModel.firstName);
    sendForm.append('lastName', sendModel.lastName);
    sendForm.append('password', sendModel.password);
    sendForm.append('id', this.selectedClaim.id),
      sendForm.append('name', this.selectedClaim.value);

    return this.authService.registerWithClaim(sendForm).subscribe(
      (response) => {
        setSavingStatus(this.onInitializing, false);
        this.onResult.emit({ status: SidebarDialogResultStatus.saveSuccess });
      },
      (responseError) => {
        setSavingStatus(this.onInitializing, false);
        this.onResult.emit({ status: SidebarDialogResultStatus.saveFail });
        this.userForm.enable();
      }
    );
  }

  //update operation
  update() {
    if(!this.selectedClaim){
      return
    }  
    this.userForm.disable();
    setUpdatingStatus(this.onInitializing, true);

    const sendModel: any = {
      userId: this.initialData.id,
      operationClaimId: this.selectedClaim.value,
    };
    console.log(sendModel);

    if (this.initialData.id) {
      this.userOperationClaimService
        .add(sendModel)
        .pipe(takeUntil(this.onDestroy))
        .subscribe(
          (response) => {
            this.onResult.emit({
              status: SidebarDialogResultStatus.updateSuccess,
            });
            setUpdatingStatus(this.onInitializing, false);
          },
          (error) => {
            this.onResult.emit({
              status: SidebarDialogResultStatus.updateFail,
            });
            this.userForm.enable();
            setUpdatingStatus(this.onInitializing, false);
          }
        );
    }
  }
}
