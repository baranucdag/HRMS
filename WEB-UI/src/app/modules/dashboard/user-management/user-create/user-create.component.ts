import { AuthService } from './../../../../core/services/api/auth.service';
import { IUser } from './../../../../core/models/views/user.model';
import { Subject, takeUntil } from 'rxjs';
import { UserService } from 'src/app/core/services/api/user.service';
import { IFormComponent } from './../../../../core/components/interfaces/form-components';
import { Component, EventEmitter, OnInit } from '@angular/core';
import { Form, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { setSavingStatus } from 'src/app/core/helpers/sidebar';
import { IOnInitializingParam } from 'src/app/core/components/models';
import {
  SidebarDialogResult,
  SidebarDialogResultStatus,
} from 'src/app/core/components/dialogs/sidebar-dialog/enums';
import { IDropdownOptions } from 'src/app/core/components/dropdowns/dropdown/models';

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
  claimTypes: any[] = [
    { id: 1, vallue: 'admin' },
    { id: 2, vallue: 'HR' },
    { id: 3, vallue: 'User' },
  ];

  get f(): any {
    return this.userForm.controls;
  }

  public readonly onInitializing = new Subject<IOnInitializingParam>();
  public readonly onResult = new EventEmitter<SidebarDialogResult>();
  private readonly onDestroy = new Subject<void>();

  constructor(
    private userService: UserService,
    private formBuilder: FormBuilder,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.createForm();
  }

  //set data (id)
  setData(data: any) {
    this.initialData = data;
    if (this.initialData.id) {
      // update
      this.getUser(this.initialData.id);
    } else {
      // create
    }
  }
  //get User by id
  getUser(id: number) {
    this.userService
      .getById(id)
      .pipe(takeUntil(this.onDestroy))
      .subscribe((response: any) => {
        const user = response.body.data;
        console.log(response.body.data);

        this.selectedUser = user;
        this.createForm(user);
      });
  }

  createForm(user?: IUser) {
    this.userForm = this.formBuilder.group({
      firstName: [user?.firstName, Validators.required],
      password: [user?.password, Validators.required],
      lastName: [user?.lastName, Validators.required],
      email: [user?.email, Validators.required],
    });
    this.setClaimDropdownOptions(this.claimTypes, this.selectedClaim);
  }

  //set dropdown options
  setClaimDropdownOptions(data: any, selected?: any) {
    // if (selected == undefined) {
    //   selected = data[0];
    //   this.selectedWorkPlaceType = selected;
    // }

    this.claimDropdownOptions = {
      items: data,
      onSelectionChange: (value) => {
        this.selectedClaim = value;
      },
      optionLabel: 'vallue',
      placeholder: 'Select',
      selected: selected,
    };
    this.claimDropdownOptions.errors?.next([]);
  }

  //add operation
  save() {
    console.log('asdsa');

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
      sendForm.append('name', this.selectedClaim.vallue);

    console.log(sendModel);
    console.log(this.selectedClaim.name);

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
}
