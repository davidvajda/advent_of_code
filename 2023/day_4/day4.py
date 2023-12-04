with open(r'D:\projects\aoc\2023\2023\2023\day_4\input_1.txt', 'r') as input_file:
    # part 1
    # card_value_sum = 0
    # for line in input_file:
    #     winning_nums, nums = [[int(num) for num in num_list if num != ''] for num_list in [x.split(" ") for x in line.split(": ")[1].replace("\n", "").split("|")]]
    #     winning_nums = set(winning_nums)
    #     
    #     card_value = 0
    #     for num in nums:
    #         if num in winning_nums:
    #             if card_value == 0:
    #                 card_value = 1
    #             else:
    #                 card_value *= 2
    #     card_value_sum += card_value
    #     
    # print(card_value_sum)
    
    # part 2
    scratchcards = [[[int(num) for num in num_list if num != ''] for num_list in [x.split(" ") for x in line.split(": ")[1].replace("\n", "").split("|")]] for line in input_file]
    scratchcard_count = [1 for sc in scratchcards]
    for i, (winning_nums, sc_nums) in enumerate(scratchcards):
        for y, num in enumerate([num for num in sc_nums if num in winning_nums]): scratchcard_count[i+1+y]+=1*scratchcard_count[i]
    print(sum(scratchcard_count))