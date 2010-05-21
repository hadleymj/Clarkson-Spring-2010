%Given an array of all the data this will process it and return
%the number of arm raises within the data set.
%for 10arm_raises.csv the offset was [0 0 28]
%The threshold in all cases can be set to 10.
function result = wii_processing(data, offset, threshold)

data = data(:,3);

[rows, cols] = size(data);

data = data(1:(rows-1),:);

%data = data(1:2:rows-2, :); 

data = Calibrate(data, offset);

single_axis = diffSteps(data, 3, threshold);

result = count_steps(single_axis);




