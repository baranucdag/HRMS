import { Subject } from 'rxjs';
import { IOnInitializingParam } from '../../components/models';
import { ITableOptions } from '../../components/tables/table/models';

export const setLoadingStatus = (
  initializeSubject: Subject<IOnInitializingParam>,
  value: boolean = false
) => {
  initializeSubject.next({ ...initializeSubject, loading: value });
};

export const setSavingStatus = (
  initializeSubject: Subject<IOnInitializingParam>,
  value: boolean = false
) => {
  initializeSubject.next({ ...initializeSubject, saving: value });
};

export const setUpdatingStatus = (
  initializeSubject: Subject<IOnInitializingParam>,
  value: boolean = false
) => {
  initializeSubject.next({ ...initializeSubject, updating: value });
};

export const setNextStatus = (
  initializeSubject: Subject<IOnInitializingParam>,
  value: boolean = false
) => {
  initializeSubject.next({ ...initializeSubject, canNext: value });
};

export const setNextHiddenStatus = (
  initializeSubject: Subject<IOnInitializingParam>,
  value: boolean = false
) => {
  initializeSubject.next({ ...initializeSubject, showNext: value });
};

export const setPreviousHiddenStatus = (
  initializeSubject: Subject<IOnInitializingParam>,
  value: boolean = false
) => {
  initializeSubject.next({ ...initializeSubject, showPrevious: value });
};


